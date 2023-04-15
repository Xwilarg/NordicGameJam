using Assets.Scripts.Hole;
using NordicGameJam.Hole;
using System;
using UnityEngine;

namespace NordicGameJam
{
    public class ColorManager : MonoBehaviour
    {
        public static ColorManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public event EventHandler<ColorEventArgs> OnColorChanged;

        public void ChangeColor(LegoColor newColor)
        {
            OnColorChanged?.Invoke(this, new() { Color = newColor });
        }
    }
}
