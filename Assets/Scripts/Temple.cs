using UnityEngine;

namespace NordicGameJam
{
    public class Temple : MonoBehaviour
    {
        private Camera _cam;

        public static Temple Instance { private set; get; }

        private void Awake()
        {
            _cam = Camera.main;
            Instance = this;
        }

        private void Update()
        {
            transform.position = new(GravityPath.CalculateBounds(_cam).min.x, transform.position.y);
        }
    }
}
