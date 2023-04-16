using NordicGameJam.Coins;
using UnityEngine;

namespace NordicGameJam.Asteroid
{
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _children;

        private void Awake()
        {
            _children[Random.Range(0, _children.Length)].SetActive(true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Temple"))
            {
                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("Player"))
            {
                CoinManager.Instance.Spawn(transform.position, Temple.Instance.transform, false);
                Destroy(gameObject);
            }
        }
    }
}
