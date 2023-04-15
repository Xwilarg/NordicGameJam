using NordicGameJam.SO;
using UnityEngine;

namespace NordicGameJam.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;

        private int _maxForce;
        private float _timer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.drag = _info.LinearDrag;
            _timer = Time.unscaledTime;
        }

        private void FixedUpdate()
        {
            if (_rb.velocity.magnitude < _info.MinSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _info.MinSpeed;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                _rb.velocity = collision.relativeVelocity;
                var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            }
        }

        public void OnForceChange(int value)
        {
            if (value == 0) // Propulse player
            {
                // Apply force to the player
                var timeDiff = Mathf.Clamp(Time.unscaledTime - _timer, 0f, _info.MaxPressDuration);
                _rb.AddForce(transform.up *
                    _info.BaseSpeed *
                    _info.PressionModifier.Evaluate(_maxForce / 100f) * // LEGO SDK always return a value between 0 and 100
                    _info.DurationModifier.Evaluate(timeDiff / 3f) *
                    Time.fixedDeltaTime
                    , ForceMode2D.Impulse);

                // Reset stuffs
                _maxForce = 0;
                _timer = Time.unscaledTime;
            }
            else if (value > _maxForce)
            {
                _maxForce = value;
            }
        }
    }
}
