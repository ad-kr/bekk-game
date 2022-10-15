using Godot;
using System;

namespace ADKR.Game
{
    public partial class Laser : Line2D
    {

        private Tween _tween;
        private AnimatedSprite2D _hit;

        public Vector2 Offset
        {
            get => Points[0];
            set
            {
                SetPointPosition(0, value);
            }
        }

        public Vector2 Target
        {
            get => Points[1];
            set
            {
                SetPointPosition(1, value);
                _hit.Position = value + new Vector2(0f, -5f);
            }
        }

        public override void _Ready()
        {
            base._Ready();
            _hit = GetNode<AnimatedSprite2D>("LaserHit");
            _tween = CreateTween().SetLoops();
            _tween.TweenProperty(this, "width", 2f, 0.2f);
            _tween.TweenProperty(this, "width", 3f, 0.2f);
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            _tween?.Kill();
        }
    }
}