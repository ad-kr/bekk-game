using Godot;
using System;

namespace ADKR.Game
{
    public abstract partial class Trigger : Area2D
    {
        private Node2D _label;
        private bool _isPlayerEntered;
        private Tween _tween;

        public override void _Ready()
        {
            base._Ready();

            Connect("body_entered", new Callable(this, nameof(OnEnter)));
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Disconnect("body_entered", new Callable(this, nameof(OnEnter)));
        }

        private void OnEnter(Node body)
        {
            if (body is not Player) return;
            Execute();
        }

        public abstract void Execute();
    }
}