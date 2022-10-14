using Godot;
using System;

namespace ADKR.Game
{
    public class EnemyDeathState : CharacterState<Enemy>
    {
        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "death";
            Char.Sprite.Playing = true;

            //Wait for next frame, in case there is an effect scheduled that modifies the modulate value.
            await Char.ToSignal(Char.GetTree(), "process_frame");

            Char.Sprite.Modulate = Colors.Red * 10f;
            Tween tween = Char.CreateTween();
            tween.TweenProperty(Char.Sprite, "modulate", Colors.White, 0.4f);

            await Char.ToSignal(Char.Sprite, "animation_finished");

            await Char.ToSignal(Char.GetTree().CreateTimer(1f), "timeout");

            Area2D scrap = ResourceLoader.Load<PackedScene>("res://combat/scrap/Scrap.tscn").Instantiate<Area2D>();
            Char.GetParent().AddChild(scrap);
            scrap.Position = Char.Position;

            Char.QueueFree();
        }
    }
}