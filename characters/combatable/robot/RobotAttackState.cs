using Godot;
using System;

namespace ADKR.Game
{
    public class RobotAttackState : CharacterState<Robot>
    {
        private readonly Combatable _target;

        private const float _laserSpeed = 32f;
        private Vector2 _laserPos;
        private Vector2 _laserVelocity = Vector2.Zero;
        private readonly float _laserThreshold = 4f;

        private readonly Attack _attack;
        private float _attackCooldown = 500f;
        private const float _attackCooldownDuration = 500f;

        private Sprite2D debugCircle;

        public RobotAttackState(Combatable target)
        {
            _target = target;
            AttackOptions attackOptions = new()
            {
                MinDamage = 10f,
                MaxDamage = 10f,
                OnHit = damage =>
                {
                    // _target.Effects.Add(new SlowDownEffect());
                    _target.ApplyEffect(new HitEffect());
                }
            };

            _attack = new Attack(attackOptions, _target);
        }

        public override void Start()
        {
            base.Start();
            Vector2 dir = Char.GlobalPosition.DirectionTo(_target.Position);
            _laserPos = Char.Position + dir * 16f;
            debugCircle = GD.Load<PackedScene>("res://debug/DebugPoint.tscn").Instantiate<Sprite2D>();
            ScreenOverlay.Instance.AddChild(debugCircle);
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            if (_target.IsDead)
            {
                Char.State = new RobotDeactivateState();
                return;
            }

            float distance = _target.Position.DistanceTo(Char.Position);
            if (distance > Char.ActivationRadius * 2f)
            {
                Char.State = new RobotRunState(_target);
                return;
            }

            Vector2 dir = _laserPos.DirectionTo(_target.Position);
            _laserVelocity = dir * _laserSpeed;
            _laserPos += _laserVelocity * (float)delta;
            debugCircle.Position = _laserPos;

            _attackCooldown += (float)delta;
            if (_attackCooldown * 1000f < _attackCooldownDuration) return;

            float laserTargetDistance = _target.Position.DistanceTo(_laserPos);
            if (laserTargetDistance <= _laserThreshold)
            {
                _attackCooldown = 0f;
                _attack.Execute();
                GD.Print(_target.Health);
            }
        }

        public override void End()
        {
            base.End();
            debugCircle.QueueFree();
        }
    }
}
