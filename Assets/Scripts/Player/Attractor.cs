using NordicGameJam;
using NordicGameJam.Hole;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public int Strenght;
    public bool Activated;

    public LegoColor Color;

    private void Start()
    {
        ColorManager.Instance.OnColorChanged += (_, e) =>
        {
            gameObject.SetActive(e.Color == Color);
        };
    }
}