using ADKR.Game;
using Godot;

namespace ADKR.Game
{
    class SlowDownEffect : CharacterEffect
    {
        public SlowDownEffect()
        {
            IsUnique = true;
        }

        public override async void Start()
        {
            base.Start();

            float originalSpeed = Char.RunSpeed;
            Char.RunSpeed *= 0.5f;

            await Char.ToSignal(Char.GetTree().CreateTimer(0.2f), "timeout");

            Char.RunSpeed = originalSpeed;
            Char.RemoveEffect(this);
        }
    }
}
