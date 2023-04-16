using UnityEngine;

namespace NordicGameJam
{
    public class BGMManager : MonoBehaviour
    {
        public static BGMManager Instance { private set; get; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
            }
        }
    }
}
