using NordicGameJam;
using NordicGameJam.Hole;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public int Strenght;
    public bool Activated;

    public LegoColor Color;
    public float MaxAttractionDistance;

    [SerializeField]
    private ColorVFXInfo[] _infos;

    private DarkMatter _vfx;

    private void Awake()
    {
        _vfx = _infos.FirstOrDefault(x => x.Key == Color).VFX.GetComponent<DarkMatter>();
        _vfx.gameObject.SetActive(true);
        _vfx.Toggle(Color == LegoColor.RED);
        Activated = Color == LegoColor.RED;
    }

    private void Start()
    {
        ColorManager.Instance.OnColorChanged += (_, e) =>
        {
            _vfx.Toggle(e.Color == Color);
            Activated = e.Color == Color;
        };
    }
}