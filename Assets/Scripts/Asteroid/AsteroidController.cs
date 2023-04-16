using NordicGameJam.Coins;
using UnityEngine;
using System.Collections;

namespace NordicGameJam.Asteroid
{
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _children;

        [SerializeField]
        private GameObject _destroyVFXContainer;

        [SerializeField]
        private float _dissolveSpeed = 2f;
        private float _dissolveAmount = .8f;
        private Material _dissolveMat;

        private void Awake()
        {
            var go = _children[Random.Range(0, _children.Length)];
            go.SetActive(true);
            _dissolveMat = go.GetComponent<Renderer>().material;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Temple"))
            {
                StartCoroutine(DissolveThenDestroy());
            }
            if (collision.collider.CompareTag("Player"))
            {
                CoinManager.Instance.Spawn(transform.position, Temple.Instance.transform, false);
                StartCoroutine(DissolveThenDestroy());
            }
        }

        private IEnumerator DissolveThenDestroy()
        {
            while (_dissolveAmount > 0)
            {
                _dissolveAmount -= Time.deltaTime * _dissolveSpeed;
                _dissolveMat.SetFloat("_Dissolve", _dissolveAmount);
                // delay the explosion VFX
                if (!vfxPlayed && _dissolveAmount < 0.5f)
                {
                    PlayDestroyVFX();
                    vfxPlayed = true;
                }
                yield return null;
            }
            Destroy(gameObject);
        }
        
        private bool vfxPlayed = false;
        private void PlayDestroyVFX()
        {
            foreach(ParticleSystem childPS in _destroyVFXContainer.GetComponentsInChildren<ParticleSystem>())
            {
                childPS.Play();
            }
        }
    }
}
