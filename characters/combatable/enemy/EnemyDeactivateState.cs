namespace ADKR.Game
{
    public class EnemyDeactivateState : CharacterState<Enemy>
    {
        public override async void Start()
        {
            base.Start();

            Char.Target = null;

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "deactivate";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            Char.State = Char.IdleState;
        }
    }
}