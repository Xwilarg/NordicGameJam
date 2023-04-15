using LEGOWirelessSDK;
using System;
using UnityEngine;

namespace NordicGameJam.Player
{
    public class SensorInfo : MonoBehaviour
    {
        private ForceSensor _forceSensor;

        private void Awake()
        {
            _forceSensor = GetComponent<ForceSensor>();
        }

        public int InstanceID => _forceSensor.GetInstanceID();

        public void OnSensorConnected(bool value)
        {
            Debug.Log(_forceSensor != null);
            OnConnected?.Invoke(this, new() { Connected = value, Sensor = _forceSensor });
        }

        public event EventHandler<PlayerConnectionEventArgs> OnConnected;
    }
}
