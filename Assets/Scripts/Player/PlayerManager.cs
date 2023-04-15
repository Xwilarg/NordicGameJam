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

        // Reference sensor ID to gameobject
        private readonly Dictionary<int, GameObject> _instances = new();

        private void Awake()
        {
            foreach (var s in _sensors)
            {
                s.OnConnected += (_, e) =>
                {
                    if (e.Connected)
                    {
                        Assert.IsFalse(_instances.ContainsKey(e.InstanceID), "Player already was instanciated");
                        var go = Instantiate(_playerPrefabs, Vector3.zero, Quaternion.identity);
                        _instances.Add(e.InstanceID, go);
                    }
                    else
                    {
                        Assert.IsTrue(_instances.ContainsKey(e.InstanceID), "Player doesn't exists");
                        Destroy(_instances[e.InstanceID]);
                        _instances.Remove(e.InstanceID);
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
