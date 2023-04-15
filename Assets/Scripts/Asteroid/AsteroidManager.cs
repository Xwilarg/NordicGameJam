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
        private Sprite[] _asteroidSprites;

        [SerializeField]
        private AsteroidInfo _info;

        [SerializeField]
        private Transform _temple;

        private void Awake()
        {
            StartCoroutine(Spawn(_info.SpawnInternal, _asteroidPrefab));
        }

        private IEnumerator Spawn(float interval, GameObject asteroid)
        {
            yield return new WaitForSeconds(interval);
            float radius = 25.0f;
            Vector3 randomPosition = Random.onUnitSphere * radius;
            if (randomPosition.x < 0f)
            {
                randomPosition = new(-randomPosition.x, randomPosition.y);
            }
            randomPosition += transform.position;
            randomPosition.z = 0.0f;

            var go = Instantiate(asteroid, new Vector2(randomPosition.x, randomPosition.y), Quaternion.identity);

            var direction = (_temple.position - go.transform.position).normalized;
            var rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = direction * _info.Speed;
            rb.angularVelocity = Random.Range(10f, 20f);
            go.GetComponent<SpriteRenderer>().sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];
            yield return Spawn(interval, asteroid);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 25.0f);
        }
    }
}
