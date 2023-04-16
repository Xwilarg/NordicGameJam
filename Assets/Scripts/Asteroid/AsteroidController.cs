using NordicGameJam.Audio;
using NordicGameJam.Coins;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SFXManager.Instance.TakeDamage();
                var amount = CoinManager.Instance.CoinLost;
                if (CoinManager.Instance.CurrentCoin < amount)
                {
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    for (int i = 0; i < amount; i++)
                    {
                        CoinManager.Instance.Spawn(Temple.Instance.CoinSpawn.position, null, true);
                        CoinManager.Instance.CurrentCoin--;
                    }
                }
                Destroy(gameObject);
            }
            if (collision.collider.CompareTag("Player"))
            {
                SFXManager.Instance.DestroyAsteroid();
                CoinManager.Instance.Spawn(transform.position, Temple.Instance.transform, false);
                Destroy(gameObject);
            }
        }
    }
}
