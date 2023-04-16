using UnityEngine;

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
    }
}
