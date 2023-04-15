using UnityEngine;

namespace NordicGameJam.Asteroid
{
    public class AsteroidController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Temple") || collision.collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
