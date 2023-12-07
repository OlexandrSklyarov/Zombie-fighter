
namespace SA.Gameplay.Enemies.FSM.States
{
    public class AttackState : UnitState
    {
        public AttackState(IUnitStateSwitcher switcher, IUnitBrainContext context) : base(switcher, context)
        {
        }

        public override void OnEnter()
        {
            _context.Animator.Attack();
            
            if (_context.Sensor.Target.IsAlive)
            {
                _context.Sensor.Target.ApplyDamage(_context.Damage);
            }

            _switcher.SwitchState<DestroyState>();
        }

        public override void OnExit()
        {
        }
    }
}