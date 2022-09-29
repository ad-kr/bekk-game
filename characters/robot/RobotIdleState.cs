using Godot;
using System;

namespace ADKR.Game
{
    public class RobotIdleState : CharacterState<Robot>
    {
        public override void Start()
        {
            base.Start();
            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "Idle";
            Char.Sprite.Playing = true;
        }
    }
}