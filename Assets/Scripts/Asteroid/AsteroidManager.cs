using NordicGameJam.SO;
using System.Collections;
using UnityEngine;

namespace NordicGameJam.Asteroid
{
    public class AsteroidManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _asteroidPrefab;

        [SerializeField]
        private AsteroidInfo _info;

        [SerializeField]
        private Transform _temple;

        private float _minX = 12.0f;
        private float _maxX = 20.0f;
        private float _minY = -6.0f;
        private float _maxY = 6.0f;

        private void Awake()
        {
            StartCoroutine(Spawn(_info.SpawnInternal, _asteroidPrefab));
        }

        private IEnumerator Spawn(float interval, GameObject asteroid)
        {
            yield return new WaitForSeconds(interval);
            Vector3 randomPosition = new Vector3( Random.Range(_minX, _maxX), Random.Range(_minY, _maxY), 0.0f);
            randomPosition += transform.position;
            randomPosition.z = 0.0f;

            var go = Instantiate(asteroid, new Vector2(randomPosition.x, randomPosition.y), Quaternion.identity);

            var direction = (_temple.position - go.transform.position).normalized;
            var rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = direction * _info.Speed;
            rb.angularVelocity = Random.Range(10f, 20f);
            yield return Spawn(interval, asteroid);
        }
    }
}
