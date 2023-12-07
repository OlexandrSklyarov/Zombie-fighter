
namespace SA.Gameplay.Enemies.FSM.States
{
    public class DestroyState : UnitState
    {
        public DestroyState(IUnitStateSwitcher switcher, IUnitBrainContext context) : base(switcher, context)
        {
        }

        public override void OnEnter()
        {
            _context.AutoDestroy();
        }

        public override void OnExit()
        {
        }
    }
}