using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private float spawnInterval = 3.0f;

    private void Awake()
    {
        StartCoroutine(Spawn(spawnInterval, asteroidPrefab));
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn(float interval, GameObject asteroid)
    {
        yield return new WaitForSeconds(interval);
        GameObject newAsteroid = Instantiate(asteroid, new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)), Quaternion.identity);
        yield return Spawn(interval, asteroid);
    }
}
