using Godot;
using System;

namespace ADKR.Game
{
    public partial class HitEffect : CharacterEffect
    {
        public HitEffect()
        {
            IsUnique = true;
        }

        public override async void Start()
        {
            base.Start();
            Tween tween = Char.CreateTween();
            Char.Sprite.Modulate = Colors.White * 4f;
            tween.TweenProperty(Char.Sprite, "modulate", Colors.White, 0.2d);
            await Char.ToSignal(tween, "finished");
            Char.RemoveEffect(this);
        }
    }
}