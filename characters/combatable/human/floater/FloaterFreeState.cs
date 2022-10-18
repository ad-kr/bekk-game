using Godot;
using System;

namespace ADKR.Game
{
    public partial class FloaterFreeState : CharacterState<Floater>
    {

        public override async void Start()
        {
            base.Start();


            await Char.ToSignal(Char.GetTree().CreateTimer(GD.RandRange(0.1f, 1f)), "timeout");

            Char.Sprite.Frame = 1;
            Char.Hair.Frame = 1;
            Char.Clothes.Frame = 1;
            Char.Sprite.Playing = true;
            Char.Hair.Playing = true;
            Char.Clothes.Playing = true;


            Tween tween = Char.CreateTween();
            tween.TweenProperty(Char, "position", Char.Position + new Vector2(0f, (float)GD.RandRange(16, 24f)), GD.RandRange(0.6, 1f));

            await Char.ToSignal(tween, "finished");

            Char.Sprite.Playing = false;
            Char.Sprite.Frame = 0;
            Char.Hair.Playing = false;
            Char.Hair.Frame = 0;
            Char.Clothes.Playing = false;
            Char.Clothes.Frame = 0;
        }
    }
}