using Godot;
using System;

namespace ADKR.Game
{
    public class BossIdleState : CharacterState<Enemy>
    {
        public override void Start()
        {
            base.Start();
            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "idle";
        }
    }
}