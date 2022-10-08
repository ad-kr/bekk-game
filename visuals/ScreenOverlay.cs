using Godot;
using System;

namespace ADKR.Game
{
    public partial class ScreenOverlay : BackBufferCopy
    {
        public static ScreenOverlay Instance { get; set; }

        public ScreenOverlay()
        {
            Instance = this;
        }
    }
}