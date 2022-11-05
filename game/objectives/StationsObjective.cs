using Godot;
using System;

namespace ADKR.Game
{
    public class StationsObjective : Objective
    {
        public override void Start()
        {
            Instruction = "Deaktiver alle stasjonene på Skuret.";

            AimIndicator.Instance.Visible = true;
            HealthBar.Instance.Visible = true;
            World.Stations.ForEach(station => station.Visible = true);

            Player.Instance.State = new PlayerIdleState();
            Player.Instance.Faction = Faction.Human;
        }
    }
}