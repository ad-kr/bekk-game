using Godot;
using System;

namespace ADKR.Game
{
    public class RobotActivateState : CharacterState<Robot>
    {
        private readonly Combatable _target;

        public RobotActivateState(Combatable target)
        {
            _target = target;
        }

        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "activate";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            Char.State = new RobotRunState(_target);
        }
    }
}