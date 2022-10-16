using Godot;
using System;
using System.Linq;

namespace ADKR.Game
{
    public class BossLandState : CharacterState<Enemy>
    {

        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 1;
            Char.Sprite.Animation = "land";
            Char.Sprite.Playing = true;
            Char.ZIndex = 1;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            if (!Char.IsDead) Char.State = new BossChargeState();
        }
    }
}