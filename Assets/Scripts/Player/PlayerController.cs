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

        // Things when we didn't move yet
        private bool _didMove;
        private bool _aimDirection;
        private float _aimTimer;

        private float _baseAngle;
        private float _startAngle, _endAngle;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.drag = _info.LinearDrag;
            _timer = Time.unscaledTime;

            _baseAngle = transform.rotation.eulerAngles.z;
            _aimTimer = _info.RotationSpeed;

            _startAngle = _baseAngle - _info.MinAngle;
            _endAngle = _baseAngle + _info.MinAngle;
        }

        private void Update()
        {
            if (!_didMove)
            {
                _aimTimer -= Time.deltaTime;
                if (_aimTimer <= 0)
                {
                    _aimTimer = _info.RotationSpeed;
                    _aimDirection = !_aimDirection;
                }

                transform.rotation = Quaternion.Euler(
                    x: transform.rotation.eulerAngles.x,
                    y: transform.rotation.eulerAngles.y,
                    z: Mathf.Lerp(_aimDirection ? _startAngle : _endAngle, _aimDirection ? _endAngle : _startAngle, _aimTimer / _info.RotationSpeed)
                );
            }
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
                var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - _baseAngle - 180f));
            }
        }

        public void OnForceChange(int value)
        {
            if (value == 0) // Propulse player
            {
                if (_maxForce > 0)
                {
                    // Apply force to the player
                    var timeDiff = Mathf.Clamp(Time.unscaledTime - _timer, 0f, _info.MaxPressDuration);
                    _rb.AddForce(transform.up *
                        _info.BaseSpeed *
                        _info.PressionModifier.Evaluate(_maxForce / 100f) * // LEGO SDK always return a value between 0 and 100
                        _info.DurationModifier.Evaluate(timeDiff / 3f) *
                        Time.fixedDeltaTime
                        , ForceMode2D.Impulse);

                    _didMove = true;
                }

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
