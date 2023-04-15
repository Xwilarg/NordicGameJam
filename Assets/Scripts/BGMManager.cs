using UnityEngine;

namespace NordicGameJam
{
    public class BGMManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
