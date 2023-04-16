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

        private int _coinCount;

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
            cc.Speed = 1f;
        }
    }
}

