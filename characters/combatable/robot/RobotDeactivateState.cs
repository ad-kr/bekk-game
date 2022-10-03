namespace ADKR.Game
{
    public class RobotDeactivateState : CharacterState<Robot>
    {
        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "deactivate";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            Char.State = new RobotIdleState();
        }
    }
}