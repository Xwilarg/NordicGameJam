using UnityEngine;

namespace NordicGameJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/AsteroidInfo", fileName = "AsteroidInfo")]
    public class AsteroidInfo : ScriptableObject
    {
        public float Speed;
        public float SpawnInternal;
        public float _minX = 12.0f;
        public float _maxX = 20.0f;
        public float _minY = -6.0f;
        public float _maxY = 6.0f;
    }
}
