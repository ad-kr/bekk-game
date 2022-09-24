using Godot;
using System;

namespace ADKR.Game
{
    public class PlayerRunState : CharacterState<Player>
    {
        private readonly Vector2 _startDir;

        public PlayerRunState(Vector2 startDir)
        {
            _startDir = startDir;
        }

        public override void Start()
        {
            base.Start();
            Character.Sprite.FlipH = IsFlipped(_startDir);
            Character.Sprite.Frame = 1;
            Character.Sprite.Playing = true;
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            Vector2 dir = InputHandler.GetAxisInput();

            if (dir.LengthSquared() <= 0f)
            {
                Character.State = new PlayerIdleState();
                return;
            }

            Character.Sprite.FlipH = IsFlipped(dir);
            Character.Velocity = dir * Character.RunSpeed;

            Character.MoveAndSlide();
        }

        public override void End()
        {
            base.End();
            Character.Sprite.Playing = false;
            Character.Sprite.Frame = 0;
        }

        private bool IsFlipped(Vector2 dir)
        {
            if (dir.x == 0) return Character.Sprite.FlipH;
            if (dir.x > 0f) return false;
            return true;
        }
    }
}