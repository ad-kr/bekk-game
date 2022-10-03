using Godot;
using System;

namespace ADKR.Game
{
    public class RobotActivateState : CharacterState<Robot>
    {
        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "activate";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            Char.State = new RobotRunState();
        }
    }
}