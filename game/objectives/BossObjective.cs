using Godot;
using System;

namespace ADKR.Game
{
    public class BossObjective : Objective
    {
        private readonly Vector2 _spawnPoint = new(120f, -436f);

        private const int CrawlerAmount = 7;

        public override void Start()
        {
            base.Start();
            Instruction = "Defeat the boss to proceed";
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
    }
}