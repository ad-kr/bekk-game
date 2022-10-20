using Godot;
using System;

namespace ADKR.Game
{
    public partial class AimIndicator : Control
    {

        public static AimIndicator Instance { get; set; }

        private TextureRect _indicator;

        public AimIndicator()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            _indicator = GetNode<TextureRect>("Indicator");
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);

            float dirLength = InputHandler.GetJoyDir().Length();
            Player.Instance.PrevDir = dirLength <= 0f ? Player.Instance.PrevDir : InputHandler.GetJoyDir();
            _indicator.Rotation = Player.Instance.PrevDir.Angle();
        }
    }
}