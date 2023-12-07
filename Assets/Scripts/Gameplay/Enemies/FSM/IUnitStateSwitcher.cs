namespace SA.Gameplay.Enemies.FSM
{
    public interface IUnitStateSwitcher
    {
        void SwitchState<T>() where T : UnitState;    
    }
}