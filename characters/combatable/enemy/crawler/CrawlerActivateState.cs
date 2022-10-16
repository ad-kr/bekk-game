using Godot;
using System;

namespace ADKR.Game
{
    public partial class CrawlerActivateState : CharacterState<Crawler>
    {
        public override void Start()
        {
            base.Start();
            Char.State = Char.RunState;
        }
    }
}