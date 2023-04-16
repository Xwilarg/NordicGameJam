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

        private float _ref;

        private void Awake()
        {
            _ref = Time.unscaledTime;
            StartCoroutine(Spawn(_asteroidPrefab));
        }

        private IEnumerator Spawn(GameObject asteroid)
        {
            var rate = _info.SpawnInternal.Evaluate(Mathf.Clamp01(Time.unscaledTime - _ref / _info.MaxTime));
            yield return new WaitForSeconds(rate);
            Vector3 randomPosition = new Vector3( Random.Range(_info._minX, _info._maxX), Random.Range(_info._minY, _info._maxY), 0.0f);
            randomPosition += transform.position;
            randomPosition.z = 0.0f;

            var go = Instantiate(asteroid, new Vector2(randomPosition.x, randomPosition.y), Quaternion.identity);
            go.transform.localScale = Vector3.one * .55f;

            var target = _temple.position;
            target.y += Random.Range(-_info.templeOffset, _info.templeOffset);

            var direction = (target - go.transform.position).normalized;
            var rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = direction * _info.Speed;
            rb.angularVelocity = Random.Range(10f, 20f);
            yield return Spawn(asteroid);
        }
    }
}
