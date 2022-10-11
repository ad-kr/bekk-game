using Godot;
using System;

namespace ADKR.Game
{
    public class RobotAttackState : CharacterState<Robot>
    {
        private const float _laserSpeed = 32f;
        private Vector2 _laserPos;
        private Vector2 _laserVelocity = Vector2.Zero;
        private readonly float _laserThreshold = 4f;

        private Attack _attack;
        private float _attackCooldown = 500f;
        private const float _attackCooldownDuration = 500f;

        private Sprite2D debugCircle;

        public override void Start()
        {
            base.Start();

            AttackOptions attackOptions = new()
            {
                MinDamage = 3f,
                MaxDamage = 6f,
                OnHit = damage =>
                {
                    Char.Target.ApplyEffect(new SlowDownEffect());
                    Char.Target.ApplyEffect(new HitEffect());
                }
            };

            _attack = new Attack(attackOptions, Char.Target);

            Vector2 dir = Char.GlobalPosition.DirectionTo(Char.Target.Position);
            _laserPos = Char.Position + dir * 16f;
            debugCircle = GD.Load<PackedScene>("res://debug/DebugPoint.tscn").Instantiate<Sprite2D>();
            ScreenOverlay.Instance.AddChild(debugCircle);
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            if (Char.Target.IsDead)
            {
                Char.State = Char.DeactivateState;
                return;
            }

            float dist = Char.Target.Position.DistanceTo(Char.Position);
            if (dist > Char.ActivationRadius * 2f)
            {
                Char.State = Char.RunState;
                return;
            }

            Vector2 dir = _laserPos.DirectionTo(Char.Target.Position);
            _laserVelocity = dir * _laserSpeed;
            _laserPos += _laserVelocity * (float)delta;
            debugCircle.Position = _laserPos;

            _attackCooldown += (float)delta;
            if (_attackCooldown * 1000f < _attackCooldownDuration) return;

            float laserTargetDistance = Char.Target.Position.DistanceTo(_laserPos);
            if (laserTargetDistance <= _laserThreshold)
            {
                _attackCooldown = 0f;
                _attack.Execute();
            }
        }

        public override void End()
        {
            base.End();
            debugCircle.QueueFree();
        }
    }
}
