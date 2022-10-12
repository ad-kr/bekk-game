using Godot;
using System;
using ADKR.Extensions;

namespace ADKR.Game
{
    public partial class Crowbar : Weapon
    {

        private float _defaultBottomAngle;

        private const float AttackRadius = 16f;

        public override void _Ready()
        {
            Sprite2D sprite = new()
            {
                Texture = GD.Load<Texture2D>("res://combat/weapon/crowbar.png"),
                Position = new Vector2(1f, -5f),
            };

            ShowBehindParent = true;

            AddChild(sprite);
        }

        public override SignalAwaiter Attack(Vector2 dir)
        {
            TopHand.Angle = -45f;
            BottomHand.Angle = 130f;

            Tween tween = CreateTween().SetParallel();
            tween.TweenMethod(value =>
            {
                float angle = (float)value;
                TopHand.Angle = angle;
            }, TopHand.Angle, 130f, 0.2f);
            tween.TweenMethod(value =>
            {
                float angle = (float)value;
                BottomHand.Angle = angle;
            }, BottomHand.Angle, _defaultBottomAngle, 0.2f);

            return ToSignal(tween, "finished");
        }
    }
}