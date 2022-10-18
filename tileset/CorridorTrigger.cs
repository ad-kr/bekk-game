using Godot;
using System;

namespace ADKR.Game
{
    public partial class CorridorTrigger : Trigger
    {
        public override async void Execute()
        {
            PlayerWalkTowardsState walk = new(new Vector2(352f, -48f));
            Player.Instance.State = walk;

            await walk.Finished;

            walk = new(new Vector2(352f, -110f));
            Player.Instance.State = walk;

            await walk.Finished;

            await Game.Wait(1f);

            DialogueBox.Talk("???", "Er..", "Er dette konsulentene?", "Hva skjedde med dem?");

            QueueFree();
        }
    }
}