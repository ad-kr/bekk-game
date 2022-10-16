using Godot;
using System;

namespace ADKR.Game
{
    public partial class CrawlerChargeState : CharacterState<Crawler>
    {
        public override void Start()
        {
            base.Start();
            Char.State = Char.AttackState;
        }
    }
}