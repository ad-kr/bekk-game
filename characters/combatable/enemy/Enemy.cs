using Godot;
using System;

namespace ADKR.Game
{
    public partial class Enemy : Combatable
    {
        public float ActivationRadius { get; set; } = 64f;
        public float AttackRadius { get; set; } = 48f;

        public NavigationAgent2D NavigationAgent { get; set; }

        public Combatable Target { get; set; }

        #region Enemy States

        public CharacterState IdleState { get; set; } = new EnemyIdleState();
        public CharacterState ActivateState { get; set; } = new EnemyActivateState();
        public CharacterState RunState { get; set; } = new EnemyRunState();
        public CharacterState ChargeState { get; set; } = new EnemyChargeState();
        public CharacterState AttackState { get; set; } = new EnemyAttackState();
        public CharacterState DeactivateState { get; set; } = new EnemyDeactivateState();
        public CharacterState DeathState { get; set; } = new EnemyDeathState();

        #endregion

        public override void _Ready()
        {
            base._Ready();
            NavigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2d");
            State = new EnemyIdleState();
        }

        public override void Die()
        {
            base.Die();
            State = DeathState;
        }
    }
}