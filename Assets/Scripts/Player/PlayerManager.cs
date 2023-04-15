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

        private void Awake()
        {
            for (int i = 0; i < _sensors.Length; i++)
            {
                var s = _sensors[i];
                _instances.Add(s.GetInstanceID(), new() { Spawn = _spawns[i % _spawns.Length] });
                s.OnConnected += (_, e) =>
                {
                    Assert.IsFalse(_instances.ContainsKey(i), "Invalid sensor ID");
                    if (e.Connected)
                    {
                        Assert.IsNull(_instances[e.InstanceID].Instance, "Player was already instanciated");
                        var go = Instantiate(_playerPrefabs, _instances[e.InstanceID].Spawn.position, Quaternion.identity);
                        _instances[e.InstanceID].Instance = go;
                    }
                    else
                    {
                        Assert.IsNotNull(_instances[e.InstanceID].Instance, "Player wasn't exists");
                        Destroy(_instances[e.InstanceID].Instance);
                        _instances[e.InstanceID].Instance = null;
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
