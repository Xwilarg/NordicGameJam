using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private float spawnInterval = 3.0f;

    [SerializeField]
    private GameObject _temple;

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
        go.GetComponent<Asteroid>().Target = _temple.transform;
        yield return Spawn(interval, asteroid);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 25.0f);
    }
}
