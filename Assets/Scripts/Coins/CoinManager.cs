using NordicGameJam.SO;
using UnityEngine;

namespace NordicGameJam.Coins
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField]
        private CoinsInfo _coinInfo;

        [SerializeField]
        private GameObject _coinPrefab;

        public int CurrentCoin { set; get; }

        public static CoinManager Instance { private set; get; }

        public void Awake()
        {
            Instance = this;
        }

        public void Spawn(Vector3 pos, Transform target, bool isActive)
        {
            var go = Instantiate(_coinPrefab, pos, Quaternion.identity);
            go.layer = isActive ? 14 : 15;
            var cc = go.GetComponent<CoinController>();
            cc.Target = target;
            cc.Speed = _coinInfo.MovementSpeed;
            go.GetComponent<Rigidbody2D>().AddForce(Random.onUnitSphere.normalized * _coinInfo.PropulsionSpeedOnAsteroidDestroy, ForceMode2D.Impulse);
        }
    }
}

