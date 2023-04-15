using NordicGameJam.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace NordicGameJam
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefabs;

        [SerializeField]
        private Transform[] _spawns;

        [SerializeField]
        private Sprite[] _sprites;

        private SensorInfo[] _sensors;

        // Reference sensor ID to gameobject
        private readonly Dictionary<int, ActivePlayerInfo> _instances = new();

        private void Connect(SensorInfo sensor, int index)
        {
            var instanceID = sensor.GetInstanceID();
            Assert.IsNull(_instances[instanceID].Instance, "Player was already instanciated");
            var go = Instantiate(_playerPrefabs, _instances[instanceID].Spawn.position, Quaternion.Euler(0f, 0f, -90f));
            go.GetComponentInChildren<SpriteRenderer>().sprite = _sprites[index % _sprites.Length];
            sensor.ForceSensor.ForceChanged.AddListener(go.GetComponent<PlayerController>().OnForceChange); // TODO: Unregister event on destroy
            _instances[instanceID].Instance = go;
        }

        private void Start()
        {
            _sensors = LEGOManager.Instance.Sensors;
            for (int i = 0; i < _sensors.Length; i++)
            {
                var s = _sensors[i];
                _instances.Add(s.GetInstanceID(), new() { Spawn = _spawns[i % _spawns.Length] });
                if (!LEGOManager.Instance.CanUseKeyboard) // Mean we are in 'LEGO' mode
                {
                    Connect(s, i);
                }
            }
        }

        private bool IsConnected(int id)
        {
            return _instances[id].Instance != null;
        }

        public void OnButton1Press(InputAction.CallbackContext value)
            => OnPlayerInput(0, value);

        public void OnButton2Press(InputAction.CallbackContext value)
            => OnPlayerInput(1, value);

        public void OnPlayerInput(int playerID, InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started && LEGOManager.Instance.CanUseKeyboard && playerID < _sensors.Length)
            {
                var s = _sensors[playerID];
                if (IsConnected(s.GetInstanceID()))
                {
                    s.ForceOverride(100);
                }
                else
                {
                    Connect(s, _instances.Count(x => x.Value.Instance != null));
                }
            }
            else if (value.phase == InputActionPhase.Canceled)
            {
                var s = _sensors[playerID];
                if (IsConnected(s.GetInstanceID()))
                {
                    s.ForceOverride(0);
                }
            }
        }
    }
}
