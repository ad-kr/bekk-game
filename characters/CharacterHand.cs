using Godot;
using System;

namespace ADKR.Game
{
    public partial class CharacterHand : Node2D
    {
        private Node2D _pivot = new();

        public Sprite2D HandSprite { get; set; } = new();

        private bool _isFlipped = false;
        public bool IsFlipped
        {
            get => _isFlipped;
            set
            {
                _isFlipped = value;
                Scale = new Vector2(value ? -1f : 1f, 1f);
                Position = PivotOffset * Scale;
            }
        }

        private Vector2 _pivotOffset = Vector2.Zero;
        public Vector2 PivotOffset
        {
            get => _pivotOffset;
            set
            {
                _pivotOffset = value;
                Position = _pivotOffset * Scale;
            }
        }

        private float _offset = 0f;
        public float Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                HandSprite.Position = new Vector2(_offset, 0f);
            }
        }

        private float _angle = 0f;
        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                _pivot.Rotation = Mathf.DegToRad(_angle);
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

            HandSprite.Texture = _texture;

            AddChild(_pivot);
            _pivot.AddChild(HandSprite);
        }
    }
}