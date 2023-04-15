using LEGOWirelessSDK;
using System;
using UnityEngine;

namespace NordicGameJam.Player
{
    public class SensorInfo : MonoBehaviour
    {
        public ForceSensor ForceSensor { private set; get; }

        private void Awake()
        {
            ForceSensor = GetComponent<ForceSensor>();
        }

        public void OnSensorConnected(bool value)
        {
            OnConnected?.Invoke(this, new() { Connected = value, Sensor = this });
        }

        public void ConnectOverride()
        {
            OnSensorConnected(true);
        }

        public void ForceOverride(int value)
        {
            ForceSensor.ForceChanged?.Invoke(value);
        }

        public event EventHandler<PlayerConnectionEventArgs> OnConnected;
    }
}
