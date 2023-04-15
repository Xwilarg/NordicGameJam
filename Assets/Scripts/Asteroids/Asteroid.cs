using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Transform Target { set; private get; }
    public Rigidbody2D rb;
    
    private float _speed = 90.0f;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            Vector2 direction = (Target.position - transform.position).normalized;
            rb.velocity = direction * _speed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Temple"))
        {
            Destroy(gameObject);
        }
    }
}
