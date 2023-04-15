using UnityEngine;

namespace NordicGameJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/CoinsInfo", fileName = "CoinsInfo")]
    public class CoinsInfo : ScriptableObject
    {
        public float xCoordinate;
        public float yCoordinate;
    }
}