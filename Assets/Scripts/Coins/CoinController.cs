using NordicGameJam.Audio;
using UnityEngine;

namespace NordicGameJam.Coins
{
    public class CoinController : MonoBehaviour
    {
        public Transform Target { set; private get; }
        public float Speed { set; private get; }

        public float DragOnAst { set; private get; }

        private float _timer;

        private void Update()
        {
            if (Target != null)
            {
                _timer += Time.deltaTime * Speed;
                transform.position = Vector3.Slerp(transform.position, Target.position, _timer);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Temple"))
            {
                CoinManager.Instance.CurrentCoin++;
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Player"))
            {
                SFXManager.Instance.GrabCoin();
                Target = Temple.Instance.transform;
                _timer = 0f;
                GetComponent<Rigidbody2D>().drag = DragOnAst;
            }
        }
    }
}
