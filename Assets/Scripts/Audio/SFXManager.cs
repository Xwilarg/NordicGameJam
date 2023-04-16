using UnityEngine;

namespace NordicGameJam.Audio
{
    public class SFXManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _source;

        [SerializeField]
        private AudioClip _coin, _explodes, _damage, _speedUp;

        public static SFXManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public void GrabCoin()
            => _source.PlayOneShot(_coin);
        public void TakeDamage()
            => _source.PlayOneShot(_damage);
        public void DestroyAsteroid()
            => _source.PlayOneShot(_explodes);
        public void SpeedUp()
            => _source.PlayOneShot(_speedUp);
    }
}
