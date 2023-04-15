using System;

namespace NordicGameJam.Player
{
    public class PlayerConnectionEventArgs : EventArgs
    {
        public bool Connected { set; get; }
        public SensorInfo Sensor { set; get; }
    }
}
