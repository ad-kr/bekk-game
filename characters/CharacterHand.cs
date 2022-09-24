using Godot;
using System;

namespace ADKR.Game
{
    public partial class CharacterHand : Node2D
    {
        private Node2D _pivot = new();

        private Sprite2D _hand = new();

        private bool _isFlipped = false;
        public bool IsFlipped
        {
            get => _isFlipped;
            set
            {
                _isFlipped = value;
                Scale = new Vector2(value ? -1f : 1f, 1f);
            }
        }

        private Texture2D _texture;

        public CharacterHand(Texture2D texture)
        {
            _texture = texture;
        }

        public override void _Ready()
        {
            base._Ready();

            _hand.Texture = _texture;

            AddChild(_pivot);
            _pivot.AddChild(_hand);
        }

        public void SetHand(float angle, float offset)
        {
            _pivot.Rotation = Mathf.DegToRad(angle);
            _hand.Position = new Vector2(offset, 0f);
        }
    }
}