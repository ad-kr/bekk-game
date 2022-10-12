using Godot;
using System;

namespace ADKR.Game
{
    public class PlayerRunState : CharacterState<Player>
    {
        private const float _topMinAngle = 45f;
        private const float _topMaxAngle = 135f;
        private const float _bottomMinAngle = 30f;
        private const float _bottomMaxAngle = 120f;

        private readonly Vector2 _startDir;

        private float _count = 0f;

        public PlayerRunState(Vector2 startDir) => _startDir = startDir;

        #region Overrides

        public override void Start()
        {
            base.Start();

            Char.IsFlipped = IsFlipped(_startDir);

            Char.Sprite.Frame = 1;
            Char.Sprite.Playing = true;
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            Vector2 dir = InputHandler.GetAxisInput();

            if (dir.LengthSquared() <= 0f)
            {
                Char.State = new PlayerIdleState();
                return;
            }

            World.RefreshTimer();

            _count += (float)delta;

            WaveHands();

            Char.IsFlipped = IsFlipped(dir);
            Char.Velocity = dir * Char.RunSpeed;

            Char.MoveAndSlide();
        }

        public override void End()
        {
            base.End();
            Char.Sprite.Playing = false;
            Char.Sprite.Frame = 0;
        }

        #endregion

        private void WaveHands()
        {
            float sine = Mathf.Sin(_count * 8f);
            float normalizedSine = sine / 2f + 0.5f;
            float topAngle = Mathf.Lerp(_topMinAngle, _topMaxAngle, normalizedSine);
            Char.TopHand.Angle = topAngle;

            float offsetSine = Mathf.Cos(_count * 8f + 8f);
            float normalizedOffsetSine = offsetSine / 2f + 0.5f;
            float bottomAngle = Mathf.Lerp(_bottomMinAngle, _bottomMaxAngle, normalizedOffsetSine);
            Char.BottomHand.Angle = bottomAngle;
        }

        private bool IsFlipped(Vector2 dir)
        {
            if (dir.x == 0) return Char.Sprite.FlipH;
            if (dir.x > 0f) return false;
            return true;
        }
    }
}