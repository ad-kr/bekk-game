using Godot;
using System;
using Object = Godot.Object;

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
        private const float AttackCooldownDuration = 500f;

        private Laser _laser;

        public override void Start()
        {
            base.Start();

            if (Char.Target == null || Char.Target.IsDead)
            {
                Char.State = Char.DeactivateState;
                return;
            }

            _attackCooldown = AttackCooldownDuration;
            _laserPos = default;
            _laserVelocity = Vector2.Zero;

            AttackOptions attackOptions = new()
            {
                MinDamage = 3f,
                MaxDamage = 6f,
                OnHit = (target, damage) =>
                {
                    target.ApplyEffect(new SlowDownEffect());
                    target.ApplyEffect(new HitEffect());
                    target.ApplyEffect(new HitMark(damage));
                }
            };

            _attack = new Attack(attackOptions, Char.Target);

            Vector2 dir = Char.GlobalPosition.DirectionTo(Char.Target.Position);
            _laserPos = Char.Position + dir * 16f;
            _laser = GD.Load<PackedScene>("res://characters/combatable/enemy/robot/Laser.tscn").Instantiate<Laser>();
            _laser.Offset = new Vector2(-2f, -2f);
            Char.AddChild(_laser);
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            if (Char.Target == null || Char.Target.IsDead)
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
            _laser.Target = _laserPos - Char.Position;

            _attackCooldown += (float)delta;
            if (_attackCooldown * 1000f < AttackCooldownDuration) return;

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
            if (Object.IsInstanceValid(_laser)) _laser?.QueueFree();
        }
    }
}
