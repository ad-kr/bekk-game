using Godot;
using System;

namespace ADKR.Game
{
    public class EnemyAttackState : CharacterState<Enemy>
    {

        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "attack";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            Char.State = Char.DeactivateState;
        }
    }
}