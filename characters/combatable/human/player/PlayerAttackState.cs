using Godot;
using System;

namespace ADKR.Game
{
    public partial class PlayerAttackState : CharacterState<Player>
    {
        private Vector2 _dir;

        public PlayerAttackState(Vector2 dir)
        {
            _dir = dir;
        }

        public override async void Start()
        {
            base.Start();

            Char.IsFlipped = IsFlipped(_dir);

            await Char.EquippedWeapon.Attack(_dir);

            Char.State = new PlayerIdleState();
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            Vector2 dir = InputHandler.GetAxisInput();

            if (dir.LengthSquared() <= 0f)
            {
                Char.Sprite.Playing = false;
                Char.Hair.Playing = false;
                Char.Clothes.Playing = false;
                return;
            }

            TimeoutReminder.RefreshTimer();

            Char.Sprite.Playing = true;
            Char.Hair.Playing = true;
            Char.Clothes.Playing = true;
            Char.Velocity = dir * Char.RunSpeed * 0.5f;

            Char.MoveAndSlide();
        }

        private bool IsFlipped(Vector2 dir)
        {
            if (dir.x == 0) return Char.Sprite.FlipH;
            if (dir.x > 0f) return false;
            return true;
        }

    }
}