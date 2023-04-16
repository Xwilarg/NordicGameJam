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


        // Start is called before the first frame update
        void Start()
        {
            Instantiate(_coinPrefab, new Vector2(_coinInfo.xCoordinate, _coinInfo.yCoordinate), Quaternion.identity);
        }
    }
}

