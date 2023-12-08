using System;
using SA.Gameplay.Player;

namespace SA.Gameplay.Enemies.FSM.States
{
    public class PatrolState : UnitState
    {
        public PatrolState(IUnitStateSwitcher switcher, IUnitBrainContext context) 
            : base(switcher, context)
        {
        }

        public override void OnEnter()
        {
            _context.Animator.Idle();
            _context.Sensor.OnDetectTargetEvent += OnLookTarget;
            _context.DamageEvent += OnTakeDamage;
        }

        public override void OnExit()
        {
            _context.Sensor.OnDetectTargetEvent -= OnLookTarget;  
            _context.DamageEvent -= OnTakeDamage;
        }

        private void OnTakeDamage()
        {
            _switcher.SwitchState<DamageState>();
        }

        private void OnLookTarget(IPlayerTarget target)
        {
            if (!target.IsAlive) return;

            _switcher.SwitchState<ChaseState>();
        }
    }
}