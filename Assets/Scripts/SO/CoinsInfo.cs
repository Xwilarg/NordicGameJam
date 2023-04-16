using UnityEngine;

namespace NordicGameJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/CoinsInfo", fileName = "CoinsInfo")]
    public class CoinsInfo : ScriptableObject
    {
        public float PropulsionSpeedOnAsteroidDestroy = 3f;
        public float MovementSpeed = .2f;
    }
}