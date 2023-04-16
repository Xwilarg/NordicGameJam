using NordicGameJam.SO;
using UnityEngine;

namespace NordicGameJam.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private int _maxForce;
        private float _timer;

        // Things when we didn't move yet
        public bool DidMove { private set; get; }
        private bool _aimDirection;
        private float _aimTimer;

        private GravityPath _path;

        private float _speed;
        private float _baseAngle;
        private float _startAngle, _endAngle;

        [SerializeField]
        private Transform _rotationTarget;

        public Transform RotationTarget => _rotationTarget;

        [SerializeField]
        private Transform _powerBar;

        private float _power;
        private float _currForce;

        private void Awake()
        {
            _timer = Time.unscaledTime;
            DidMove = false;

            _baseAngle = transform.rotation.eulerAngles.z;
            _aimTimer = _info.RotationSpeed;

            _startAngle = _baseAngle - _info.MinAngle;
            _endAngle = _baseAngle + _info.MinAngle;

            _path = gameObject.GetComponent<GravityPath>();
            _powerBar.localScale = new Vector3(0f, _powerBar.localScale.y, _powerBar.localScale.z);
        }

        public float Speed01
        {
            get
            {
                var minSpeed = _info.MinSpeed * (_maxForce > 0 ? _info.SlowDownMultiplier : 1f);
                return (_speed - minSpeed) / (_info.MaxSpeed - minSpeed);
            }
        }

        public float TheoricalSpeed01
        {
            get
            {
                var minSpeed = _info.MinSpeed * (_maxForce > 0 ? _info.SlowDownMultiplier : 1f);
                var nextSpeed = Mathf.Clamp(_speed + _info.BaseSpeed * _power * Time.fixedDeltaTime, minSpeed, _info.MaxSpeed);
                return (nextSpeed - minSpeed) / (_info.MaxSpeed - minSpeed);
            }
        }

        private void Update()
        {
            _power += Mathf.Clamp(Time.deltaTime *
                _info.PressionModifier.Evaluate(_currForce / 100f), // LEGO SDK always return a value between 0 and 100
            0f, _info.MaxPressDuration);
            _powerBar.localScale = new Vector3(_power / _info.MaxPressDuration, _powerBar.localScale.y, _powerBar.localScale.z);

            if (!DidMove)
            {
                _aimTimer -= Time.deltaTime;
                if (_aimTimer <= 0)
                {
                    _aimTimer = _info.RotationSpeed;
                    _aimDirection = !_aimDirection;
                }

                _rotationTarget.rotation = Quaternion.Euler(
                    x: transform.rotation.eulerAngles.x,
                    y: transform.rotation.eulerAngles.y,
                    z: Mathf.Lerp(_aimDirection ? _startAngle : _endAngle, _aimDirection ? _endAngle : _startAngle, _aimTimer / _info.RotationSpeed)
                );

                _path.SetVisualMomentum(_rotationTarget.up);
            }
            else
            {
                float angle = Mathf.Atan2(_path.CurrentMomentum.y, _path.CurrentMomentum.x) * Mathf.Rad2Deg;
                _rotationTarget.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
        }

        private void FixedUpdate()
        {
            _speed /= (1f + _info.LinearDrag * Time.deltaTime);

            var minSpeed = _info.MinSpeed * (_maxForce > 0 ? _info.SlowDownMultiplier : 1f);
            // Player has a minimal speed it can't go under
            if (_speed < minSpeed)
            {
                _speed = minSpeed;
            }
            if (_speed > _info.MaxSpeed)
            {
                _speed = _info.MaxSpeed;
            }

            _path.PathSpeed = _speed;
        }

        public void OnForceChange(int value)
        {
            if (value == 0) // Propulse player
            {
                if (_maxForce > 0)
                {
                    // Apply force to the player
                    _speed += _info.BaseSpeed * _power * Time.fixedDeltaTime;

                    DidMove = true;
                }

                // Reset stuffs
                _maxForce = 0;
                _powerBar.localScale = new Vector3(0f, _powerBar.localScale.y, _powerBar.localScale.z);
                _power = 0f;
            }
            else
            {
                if (value > _maxForce)
                {
                    if (_maxForce == 0) // Slow down the player while we are pressing the button
                    {
                        _speed = _speed * _info.SlowDownMultiplier;
                    }
                    _maxForce = value;
                }
            }
            _currForce = value;
        }
    }
}
