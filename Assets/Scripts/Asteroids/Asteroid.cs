using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private GameObject _spawnTarget;


    public Rigidbody2D rb;

    private float speed = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_spawnTarget != null)
        {
            Vector2 target = _spawnTarget.transform.position;
            Vector2 direction = (target - rb.position).normalized;
            Debug.Log("Direction");
            Debug.Log(target);
            Debug.Log(rb.position);
            Debug.Log(direction);
            rb.velocity = direction * speed * Time.deltaTime;
        }
    }
}
