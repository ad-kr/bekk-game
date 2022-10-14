using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ADKR.Game
{
    public partial class Character : CharacterBody2D
    {

        public AnimatedSprite2D Sprite { get; set; }

        public float RunSpeed { get; set; } = 64f;

        private CharacterState _state;

        public CharacterState State
        {
            get => _state;
            set
            {
                _state?.End();
                _state = value;
                _state.SetCharacter(this);
                _state?.Start();
            }
        }

        private readonly List<CharacterEffect> _effects = new();

        private bool _isFlipped;
        public bool IsFlipped
        {
            get => _isFlipped;
            set
            {
                _isFlipped = value;
                SetFlip(value);
            }
        }

        public override void _Ready()
        {
            base._Ready();
            Sprite = GetNode<AnimatedSprite2D>("Sprite");
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            State?.Update(delta);
            _effects.ForEach(effect => effect.Update(delta));
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            State?.PhysicsUpdate(delta);
            _effects.ForEach(effect => effect.PhysicsUpdate(delta));
        }

        public override void _Input(InputEvent e)
        {
            base._Input(e);
            State?.Input(e);
        }

        public void ApplyEffect(CharacterEffect effect)
        {
            bool containsType = _effects.Any(eff => eff.GetType() == effect.GetType());

            if (effect.IsUnique && containsType) return;
            _effects.Add(effect);
            effect.SetCharacter(this);
            effect.Start();
        }

        public void RemoveEffect(CharacterEffect effect)
        {
            if (!_effects.Contains(effect)) return;
            effect.End();
            _effects.Remove(effect);
        }

        protected virtual void SetFlip(bool isFlipped)
        {
            Sprite.FlipH = isFlipped;
        }

    }
}
