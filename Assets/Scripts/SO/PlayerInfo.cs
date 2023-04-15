using UnityEngine;

namespace NordicGameJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        [Header("Speed")]
        [Tooltip("Base speed of the player")]
        public float BaseSpeed;

        [Tooltip("Speed when we are slowed down (aka pressing the button)")]
        [Range(0f, 1f)]
        public float SlowDownMultiplier;

        [Tooltip("Curve evolution of the speed depending of how much we press the button")]
        public AnimationCurve PressionModifier;

        [Tooltip("How long should we press to get the max speed")]
        public float MaxPressDuration;

        [Tooltip("Curve evolution of the speed depending of how long we press the button")]
        public AnimationCurve DurationModifier;

        [Range(0f, 1f)]
        [Tooltip("Drag applied to the player")]
        public float LinearDrag;

        [Tooltip("Speed the player won't go under")]
        public float MinSpeed;
        public float MaxSpeed;

        [Header("Aim")]
        public float RotationSpeed;

        public float MinAngle;
    }
}
