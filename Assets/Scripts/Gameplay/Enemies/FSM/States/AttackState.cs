
using UnityEngine;

namespace SA.Gameplay.Enemies.FSM.States
{
    public class AttackState : UnitState
    {
        private float _attackDelay;

        public AttackState(IUnitStateSwitcher switcher, IUnitBrainContext context) : base(switcher, context)
        {
        }

        public override void OnEnter()
        {
            _attackDelay = _context.Animator.Attack();  
        }

        public override void OnUpdate()
        {
            if (_attackDelay > 0f)
            {
                _attackDelay -= Time.deltaTime;
                return;
            }

            if (_context.Sensor.Target.IsAlive && IsCloseTarget())
            {
                _context.Sensor.Target.ApplyDamage(_context.Damage);
            }

            _switcher.SwitchState<DestroyState>();
        }

        public override void OnExit()
        {
        }

        private bool IsCloseTarget()
        {
            var sqDist = (_context.Sensor.Target.Position - _context.MyTransform.position).sqrMagnitude;
            return sqDist <= _context.AttackDistance * _context.AttackDistance;
        }
    }
}