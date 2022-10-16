using Godot;
using System;

namespace ADKR.Game
{
    public partial class CrawlerDeactivateState : CharacterState<Crawler>
    {
        public override void Start()
        {
            base.Start();
            Char.State = Char.IdleState;
        }
    }
}