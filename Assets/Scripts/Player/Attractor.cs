using NordicGameJam;
using NordicGameJam.Hole;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public int Strenght;
    public bool Activated;

    public LegoColor Color;

    [SerializeField]
    private ColorVFXInfo[] _infos;

    private void Awake()
    {
        _infos.FirstOrDefault(x => x.Key == Color).VFX.SetActive(true);
    }

    private void Start()
    {
        ColorManager.Instance.OnColorChanged += (_, e) =>
        {
            gameObject.SetActive(e.Color == Color);
        };
    }
}