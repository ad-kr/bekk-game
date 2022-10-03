using Godot;
using ADKR.Extensions;

namespace ADKR.Game
{
    public class PlayerIdleState : CharacterState<Player>
    {
        private const float _topSettlePoint = 90f;
        private const float _bottomSettlePoint = 45f;

        private Tween _tween;

        private const float _settleSpeed = 128f;

        public override void Start()
        {
            base.Start();

            float settleTime = Mathf.Abs(Char.TopHand.Angle - _topSettlePoint) / _settleSpeed;

            _tween = Char.CreateTween();

            _tween.Parallel()
                .TweenMethod(delta => Char.TopHand.Angle = (float)delta, Char.TopHand.Angle, _topSettlePoint, settleTime)
                .SetTrans(Tween.TransitionType.Back);
            _tween.Parallel()
                .TweenMethod(delta => Char.BottomHand.Angle = (float)delta, Char.BottomHand.Angle, _bottomSettlePoint, settleTime)
                .SetTrans(Tween.TransitionType.Back);
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            Vector2 dir = InputHandler.GetAxisInput();

            if (dir.LengthSquared() <= 0f) return;

            Char.State = new PlayerRunState(dir);
        }

        public override void End()
        {
            _tween.Kill();
        }
    }
}
