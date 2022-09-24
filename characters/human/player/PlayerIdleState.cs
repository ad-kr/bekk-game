using Godot;
using ADKR.Extensions;

namespace ADKR.Game
{
    public class PlayerIdleState : CharacterState<Player>
    {
        private Tween _handsTween;

        private readonly float _settleSpeed = 128f;

        public override void Start()
        {
            base.Start();

            float topSettlePoint = 90f;
            float topSettleTime = Mathf.Abs(Character.TopHand.Angle - topSettlePoint) / _settleSpeed;

            float bottomSettlePoint = 45f;
            float bottomSettleTime = Mathf.Abs(Character.BottomHand.Angle - bottomSettlePoint) / _settleSpeed;

            _handsTween = Character.CreateTween();
            _handsTween.Parallel().TweenMethod(delta =>
            {
                Character.TopHand.Angle = (float)delta;
            }, Character.TopHand.Angle, topSettlePoint, topSettleTime).SetTrans(Tween.TransitionType.Back);

            _handsTween.Parallel().TweenMethod(delta =>
            {
                Character.BottomHand.Angle = (float)delta;
            }, Character.BottomHand.Angle, bottomSettlePoint, bottomSettleTime).SetTrans(Tween.TransitionType.Back);
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            Vector2 dir = InputHandler.GetAxisInput();

            if (dir.LengthSquared() <= 0f) return;

            Character.State = new PlayerRunState(dir);
        }

        public override void End()
        {
            _handsTween.Kill();
        }
    }
}
