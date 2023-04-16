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

        public AnimationCurve AttractionCurve;

        [Tooltip("Curve evolution of the speed depending of how much we press the button")]
        public AnimationCurve PressionModifier;

        [Tooltip("How long should we press to get the max speed")]
        public float MaxPressDuration;

        [Range(0f, 1f)]
        [Tooltip("Drag applied to the player")]
        public float LinearDrag;

        [Tooltip("Speed the player won't go under")]
        public float MinSpeed;
        public float MaxSpeed;

        [Header("Aim")]
        public float RotationSpeed;

        public float MinAngle;

        [Header("Path Ahead")]
        [Range(0f, 40f)]
        [Tooltip("Distance ahead predicted by path")]
        public float DistAhead;

        [Tooltip("Time resolution of path simulation")]
        [Range(0.01f, 0.4f)]
        public float TimeStep;

        public bool UseForce;
    }
}
