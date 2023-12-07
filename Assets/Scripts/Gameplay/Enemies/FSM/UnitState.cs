
namespace SA.Gameplay.Enemies.FSM
{
    public abstract class UnitState
    {
        protected readonly IUnitStateSwitcher _switcher;
        protected readonly IUnitBrainContext _context;

        public UnitState(IUnitStateSwitcher switcher, IUnitBrainContext context)
        {
            _switcher = switcher;
            _context = context;
        }

        public abstract void OnEnter();
        public virtual void OnUpdate() {}
        public abstract void OnExit();
    }
}