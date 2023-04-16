using UnityEngine;

namespace NordicGameJam.Coins
{
    public class CoinController : MonoBehaviour
    {
        public Transform Target { set; private get; }
        public float Speed { set; private get; }

        private float _timer;

        private void Update()
        {
            if (Target != null)
            {
                _timer += Time.deltaTime * Speed;
                transform.position = Vector3.Slerp(transform.position, Target.position, _timer);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Temple"))
            {
                Debug.Log("HU");
            }
        }
    }
}
