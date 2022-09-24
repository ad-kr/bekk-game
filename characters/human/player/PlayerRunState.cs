using Godot;
using System;

namespace ADKR.Game
{
    public class PlayerRunState : CharacterState<Player>
    {
        private readonly Vector2 _startDir;

        private float _count;

        public PlayerRunState(Vector2 startDir) => _startDir = startDir;

        public override void Start()
        {
            base.Start();

            SetPlayerFlip(_startDir);

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


            _count += (float)delta;



            float sine = ((float)Math.Sin(_count * 8f) / 2f) + 0.5f;
            float sine2 = ((float)Math.Cos(_count * 8f + 8f) / 2f) + 0.5f;
            float angle = Mathf.Lerp(45f, 135f, sine);
            float angle2 = Mathf.Lerp(30f, 120f, sine2);
            Character.TopHand.Angle = angle;
            Character.BottomHand.Angle = angle2;

            SetPlayerFlip(dir);

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

        private void SetPlayerFlip(Vector2 dir)
        {
            bool isFlipped = IsFlipped(dir);
            Character.Sprite.FlipH = isFlipped;
            Character.TopHand.IsFlipped = isFlipped;
            Character.BottomHand.IsFlipped = isFlipped;
        }
    }
}