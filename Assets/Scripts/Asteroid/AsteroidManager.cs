using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace NordicGameJam.Asteroid
{
    public class AsteroidManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject asteroidPrefab;

        [SerializeField]
        private float spawnInterval = 3.0f;

        [SerializeField]
        private Transform _temple;

        private float _speed = .9f;

        private void Awake()
        {
            StartCoroutine(Spawn(spawnInterval, asteroidPrefab));
        }

        private IEnumerator Spawn(float interval, GameObject asteroid)
        {
            yield return new WaitForSeconds(interval);
            float radius = 25.0f;
            Vector3 randomPosition = UnityEngine.Random.onUnitSphere * radius;
            randomPosition += transform.position;
            randomPosition.z = 0.0f;

            var go = Instantiate(asteroid, new Vector2(randomPosition.x, randomPosition.y), Quaternion.identity);

            var direction = (_temple.position - go.transform.position).normalized;
            go.GetComponent<Rigidbody2D>().velocity = direction * _speed;
            yield return Spawn(interval, asteroid);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 25.0f);
        }
    }
}
