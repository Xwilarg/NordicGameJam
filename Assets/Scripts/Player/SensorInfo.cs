using System;
using UnityEngine;

namespace NordicGameJam.Player
{
    public class SensorInfo : MonoBehaviour
    {
        public void OnSensorConnected(bool value)
        {
            OnConnected?.Invoke(this, new() { Connected = value, InstanceID = GetInstanceID() });
        }

        public event EventHandler<PlayerConnectionEventArgs> OnConnected;
    }
}
