using Godot;
using System;
using System.Linq;

namespace ADKR.Game
{
    public class BossChargeState : CharacterState<Enemy>
    {

        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "charge";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            Char.Target = Player.Instance;

            if (!Char.IsDead) Char.State = new BossJumpState();
        }
    }
}