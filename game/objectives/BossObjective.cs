using Godot;
using System;

namespace ADKR.Game
{
    public class BossObjective : Objective
    {
        private readonly Vector2 _spawnPoint = new(120f, -436f);

        private const int CrawlerAmount = 7;

        public override async void Start()
        {
            base.Start();
            Instruction = "Bekjempe den sentrale AI-en for å frigjøre konsulentene";

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(0.4f), "timeout");

            await ShakeScreen();

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(1f), "timeout");

            Boss.Instance.State = new BossActivateState();

            PackedScene crawlerPrefab = ResourceLoader.Load<PackedScene>("res://characters/combatable/enemy/crawler/Crawler.tscn");
            Node2D bossParent = Boss.Instance.GetParent<Node2D>();

            SpawnCrawler(CrawlerAmount, crawlerPrefab, bossParent);
        }

        private async void SpawnCrawler(int left, PackedScene prefab, Node2D parent)
        {
            if (left == 0) return;

            await Boss.Instance.ToSignal(Boss.Instance.GetTree().CreateTimer(0.6f), "timeout");

            Crawler instance = prefab.Instantiate<Crawler>();
            instance.Position = _spawnPoint;
            instance.ActivationRadius = 196f;
            parent.AddChild(instance);

            SpawnCrawler(left - 1, prefab, parent);
        }

        private static SignalAwaiter ShakeScreen()
        {
            GD.Randomize();
            Vector2 rand1 = new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized() * (float)GD.RandRange(4f, 8f);
            Vector2 rand2 = new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized() * (float)GD.RandRange(4f, 8f);
            Tween tween = Game.Instance.CreateTween();

            const float Duration = 0.1f;
            tween.TweenProperty(Camera.Instance, "offset", rand1, Duration);
            tween.TweenProperty(Camera.Instance, "offset", rand2, Duration);
            tween.TweenProperty(Camera.Instance, "offset", Vector2.Zero, Duration);
            tween.TweenProperty(Camera.Instance, "offset", rand1, Duration);
            tween.TweenProperty(Camera.Instance, "offset", rand2, Duration);
            tween.TweenProperty(Camera.Instance, "offset", Vector2.Zero, Duration);

            return Game.Instance.ToSignal(tween, "finished");
        }
    }
}