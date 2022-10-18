using Godot;
using System;

namespace ADKR.Game
{
    public partial class ScreenCover : ColorRect
    {
        public static ScreenCover Instance { get; set; }

        public ScreenCover()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            base._Ready();
			Visible = true;
            Color = new Color(Colors.Black, 0f);
        }
    }
}