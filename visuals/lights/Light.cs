using Godot;
using System;
using Object = Godot.Object;

namespace ADKR.Game
{
    public partial class Light : Sprite2D
    {
        private Node2D _bindedChar;

        public override async void _Ready()
        {
            base._Ready();
            _bindedChar = GetParent<Node2D>();

            await ToSignal(GetTree(), "process_frame");

            _bindedChar.RemoveChild(this);
            if (IsInstanceValid(ScreenOverlay.Instance)) ScreenOverlay.Instance?.AddChild(this);
        }

        public override void _PhysicsProcess(double delta)
        {
            base._Process(delta);
            if (!IsInstanceValid(_bindedChar))
            {
                QueueFree();
                return;
            }
            Position = _bindedChar.Position;
        }
    }
}
