using UnityEngine;

namespace SA.Gameplay.Enemies.FSM.States
{
    public class DamageState : UnitState
    {
        private float _damageDelay;

        public DamageState(IUnitStateSwitcher switcher, IUnitBrainContext context) : base(switcher, context)
        {
        }

        public override void OnEnter()
        {
            _damageDelay = _context.Animator.Damage();  
        }

        public override void OnUpdate()
        {
            if (_damageDelay > 0f)
            {
                _damageDelay -= Time.deltaTime;
                return;
            }

            if (_context.Sensor.Target != null)
            {
                _switcher.SwitchState<ChaseState>();
            }
            else
            {
                _switcher.SwitchState<PatrolState>();
            }            
        }

        public override void OnExit()
        {
        }
    }
}