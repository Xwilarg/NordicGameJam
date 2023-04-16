using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace NordicGameJam.Hole
{
    public class DarkMatter : MonoBehaviour
    {
        [SerializeField]
        private GameObject _trail, _sparkles, _light;

        public void Toggle(bool value)
        {
            _trail.SetActive(value);
            _sparkles.SetActive(true);
            _light.SetActive(true);
        }

        public void DisableAll()
        {
            _trail.SetActive(false);
            _sparkles.SetActive(false);
            _light.SetActive(false);
        }
    }
}
