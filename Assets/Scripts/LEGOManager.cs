using UnityEngine;

namespace NordicGameJam
{
    public class LEGOManager : MonoBehaviour
    {
        public static LEGOManager Instance { private set; get; }

        private void Awake()
        {
            if (Instance != null && Instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
            }
            else if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}
