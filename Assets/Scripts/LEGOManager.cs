using LEGOWirelessSDK;
using NordicGameJam.Hole;
using NordicGameJam.Player;
using System.Linq;
using UnityEngine;

namespace NordicGameJam
{
    public class LEGOManager : MonoBehaviour
    {
        public static LEGOManager Instance { private set; get; }

        public HubBase HUB { private set; get; }

        public SensorInfo[] Sensors => GetComponentsInChildren<SensorInfo>();

        public bool CanUseKeyboard => _debugMode || !HUB.enabled;

        [SerializeField]
        private bool _debugMode;

        private void Awake()
        {
            HUB = GetComponent<HubBase>();
            if (Instance != null && Instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
            }
            else if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void ChangeLEDColor(Color c)
        {
            HUB.LedColor = c;
        }

        public void Disable()
        {
            HUB.enabled = false;
        }

        private readonly LegoColor[] _validColors = new[] { LegoColor.BLUE, LegoColor.RED, LegoColor.GREEN };
        public void OnColorChange(int id)
        {
            Debug.Log(id);
            var color = (LegoColor)id;
            if (_validColors.Contains(color))
            {
                ColorManager.Instance.ChangeColor(color);
                Color? targetColor = color switch
                {
                    LegoColor.RED => Color.red,
                    LegoColor.GREEN => Color.green,
                    LegoColor.BLUE => Color.blue,
                    _ => null
                };
                if (targetColor != null)
                {
                    ChangeLEDColor(targetColor.Value);
                }
            }
        }
    }
}