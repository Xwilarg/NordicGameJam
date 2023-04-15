using UnityEngine;

namespace NordicGameJam
{
    public class Temple : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            transform.position = new(GravityPath.CalculateBounds(_cam).min.x, transform.position.y);
        }
    }
}
