
namespace SA.Gameplay.Enemies.FSM.States
{
    public class ChaseState : UnitState
    {
        public ChaseState(IUnitStateSwitcher switcher, IUnitBrainContext context) 
            : base(switcher, context)
        {
        }

        public override void OnEnter()
        {
            _context.Animator.Move();
        }

        public override void OnUpdate()
        {
            if (_context.Sensor.Target.IsAlive)
            {
                if (IsCloseTarget())
                {
                    _switcher.SwitchState<AttackState>();
                }
                else
                {
                    _context.Engine.Move(_context.Sensor.Target.Position);
                }
            }
        }

        private bool IsCloseTarget()
        {
            var sqDist = (_context.Sensor.Target.Position - _context.MyTransform.position).sqrMagnitude;
            return sqDist <= _context.AttackDistance * _context.AttackDistance;
        }

        public override void OnExit()
        {            
            _context.Animator.Idle();
        }
    }
}