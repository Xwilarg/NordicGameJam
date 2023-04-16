using NordicGameJam.Audio;
using System.Collections;
using UnityEngine;

namespace NordicGameJam.Coins
{
    public class CoinController : MonoBehaviour
    {
        public Transform Target { set; private get; }
        public float Speed { set; private get; }

        public float DragOnAst { set; private get; }

        private SpriteRenderer _sr;

        private float _timer;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            StartCoroutine(Blink());
        }

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

        private IEnumerator Blink()
        {
            yield return new WaitForSeconds(7f);
            while (true)
            {
                _sr.enabled = false;
                yield return new WaitForSeconds(.5f);
                _sr.enabled = true;
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
