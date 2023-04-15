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
            if (collision.collider.CompareTag("Temple") || collision.collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
