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

    private GameObject _vfx;

    private void Awake()
    {
        _vfx = _infos.FirstOrDefault(x => x.Key == Color).VFX;
        _vfx.SetActive(true);

        _vfx = _vfx.GetComponentsInChildren<ParticleSystem>().FirstOrDefault(x => x.CompareTag("ParticleLight")).gameObject;
        _vfx.SetActive(Color == LegoColor.RED);
        Activated = Color == LegoColor.RED;
    }

    private void Start()
    {
        ColorManager.Instance.OnColorChanged += (_, e) =>
        {
            _vfx.SetActive(e.Color == Color);
            Activated = e.Color == Color;
        };
    }
}