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
        }

        public override void OnExit()
        {
            _context.Sensor.OnDetectTargetEvent -= OnLookTarget;            
        }

        private void OnLookTarget(IPlayerTarget target)
        {
            if (!target.IsAlive) return;

            _switcher.SwitchState<ChaseState>();
        }
    }
}