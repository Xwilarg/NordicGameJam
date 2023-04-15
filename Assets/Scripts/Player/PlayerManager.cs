using NordicGameJam.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace NordicGameJam
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefabs;

        [SerializeField]
        private GameObject _waitingText;

        [SerializeField]
        private SensorInfo[] _sensors;

        [SerializeField]
        private Transform[] _spawns;

        // Reference sensor ID to gameobject
        private readonly Dictionary<int, ActivePlayerInfo> _instances = new();

        private void Start()
        {
            for (int i = 0; i < _sensors.Length; i++)
            {
                var s = _sensors[i];
                _instances.Add(s.InstanceID, new() { Spawn = _spawns[i % _spawns.Length] });
                s.OnConnected += (_, e) =>
                {
                    var instanceID = e.Sensor.GetInstanceID();
                    Assert.IsTrue(_instances.ContainsKey(instanceID), "Invalid sensor ID");
                    if (e.Connected)
                    {
                        Assert.IsNull(_instances[instanceID].Instance, "Player was already instanciated");
                        var go = Instantiate(_playerPrefabs, _instances[instanceID].Spawn.position, Quaternion.identity);
                        e.Sensor.ForceChanged.AddListener(go.GetComponent<PlayerController>().OnForceChange); // TODO: Unregister event on destroy
                        _instances[instanceID].Instance = go;
                    }
                    else
                    {
                        Assert.IsNotNull(_instances[instanceID].Instance, "Player wasn't exists");
                        Destroy(_instances[instanceID].Instance);
                        _instances[instanceID].Instance = null;
                    }
                };
            }
        }

        public void OnLegoDeviceConnected(bool value)
        {
            if (value)
            {
                _waitingText.SetActive(false);
            }
        }
    }
}
