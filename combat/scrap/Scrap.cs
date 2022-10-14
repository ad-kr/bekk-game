using Godot;
using System;

namespace ADKR.Game
{
    public partial class Scrap : Area2D
    {

        private Sprite2D _sprite;
        private Sprite2D _lootShadow;
        private Tween _tween;

        public override async void _Ready()
        {
            _sprite = GetNode<Sprite2D>("Scrap");
            _lootShadow = GetNode<Sprite2D>("LootShadow");

            Connect("body_entered", new Callable(this, nameof(OnEnter)));

            _tween = CreateTween();
            _tween.TweenProperty(_sprite, "position", new Vector2(0f, -16f), 0.3f).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
            _tween.Parallel().TweenProperty(_lootShadow, "scale", new Vector2(0.5f, 0.5f), 0.3f).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
            _tween.TweenProperty(_sprite, "position", new Vector2(0f, 0f), 0.2f);
            _tween.Parallel().TweenProperty(_lootShadow, "scale", new Vector2(0.75f, 0.75f), 0.2f);

            await ToSignal(_tween, "finished");

            _tween = CreateTween().SetLoops();
            _tween.TweenProperty(_sprite, "position", new Vector2(0f, -2f), 2f);
            _tween.Parallel().TweenProperty(_lootShadow, "scale", new Vector2(0.65f, 0.65f), 2f);
            _tween.TweenProperty(_sprite, "position", new Vector2(0f, 0f), 2f);
            _tween.Parallel().TweenProperty(_lootShadow, "scale", new Vector2(0.75f, 0.75f), 2f);
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Disconnect("body_entered", new Callable(this, nameof(OnEnter)));
        }

        private async void OnEnter(Node body)
        {
            if (body is Player player)
            {
                _tween.Kill();

                _tween = CreateTween().SetParallel();
                _tween.TweenProperty(this, "position", player.Position, 0.2f);
                _tween.TweenProperty(this, "scale", new Vector2(0.25f, 0.25f), 0.2f);

                await ToSignal(_tween, "finished");
                ScrapCounter.Instance.Count++;
                QueueFree();
            }
        }
    }

}