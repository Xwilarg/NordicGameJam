using NordicGameJam.SO;
using TMPro;
using UnityEngine;

namespace NordicGameJam.Coins
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField]
        private CoinsInfo _coinInfo;

        [SerializeField]
        private GameObject _coinPrefab;

        [SerializeField]
        private TMP_Text _coinText;

        private int _currentCoin;
        public int CurrentCoin
        {
            set
            {
                _currentCoin = value;
                _coinText.text = $"{_currentCoin}";
            }
            get => _currentCoin;
        }

        public static CoinManager Instance { private set; get; }

        public int CoinLost => _coinInfo.CoinLostOnImpact;

        public void Awake()
        {
            Instance = this;
            CurrentCoin = _coinInfo.BaseCoins;
        }

        public void Spawn(Vector3 pos, Transform target, bool isActive)
        {
            var go = Instantiate(_coinPrefab, pos, Quaternion.identity);
            go.layer = isActive ? 14 : 15;
            var cc = go.GetComponent<CoinController>();
            cc.Target = target;
            cc.Speed = _coinInfo.MovementSpeed;
            var rb = go.GetComponent<Rigidbody2D>();
            rb.AddForce(Random.onUnitSphere.normalized * (isActive ? _coinInfo.PropulsionSpeedOnBaseDamage : _coinInfo.PropulsionSpeedOnAsteroidDestroy), ForceMode2D.Impulse);
            rb.drag = isActive ? _coinInfo.DragOnBaseDamage : _coinInfo.DragOnAsteroidDestroy;
            cc.DragOnAst = _coinInfo.DragOnAsteroidDestroy;
            if (isActive)
            {
                if (rb.velocity.x < 0f)
                {
                    rb.velocity = new(-rb.velocity.x, rb.velocity.y);
                }
            }
            Destroy(go, 10f);
        }
    }
}

