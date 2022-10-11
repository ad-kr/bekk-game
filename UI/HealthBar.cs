using Godot;
using System;

namespace ADKR.Game
{
    public partial class HealthBar : TextureProgressBar
    {
        public static HealthBar Instance { get; set; }

        public HealthBar()
        {
            Instance = this;
        }

        public void SetMinMax(int min, int max)
        {
            MinValue = min;
            MaxValue = max;
        }

        public void SetValue(float value)
        {
            Value = value;
        }
    }
}