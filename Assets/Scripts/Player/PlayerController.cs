using UnityEngine;

namespace NordicGameJam.Player
{
    public class PlayerController : MonoBehaviour
    {
        public void OnForceChange(int value)
        {
            Debug.Log(value);
        }
    }
}
