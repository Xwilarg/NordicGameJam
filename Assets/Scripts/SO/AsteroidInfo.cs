using UnityEngine;

namespace NordicGameJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/AsteroidInfo", fileName = "AsteroidInfo")]
    public class AsteroidInfo : ScriptableObject
    {
        public float Speed;
        public float SpawnInternal;
    }
}
