using SA.Gameplay.Player;

namespace SA.Gameplay.Enemies.FSM.States
{
    public class WaitState : UnitState
    {
        public WaitState(IUnitStateSwitcher switcher, IUnitBrainContext context) 
            : base(switcher, context)
        {
        }

        public override void OnEnter()
        {            
        }

        public override void OnExit()
        {           
        }
    }
}