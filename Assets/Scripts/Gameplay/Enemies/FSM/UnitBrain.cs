using System.Collections.Generic;
using System.Linq;
using SA.Gameplay.Enemies.FSM.States;

namespace SA.Gameplay.Enemies.FSM
{
    public class UnitBrain : IUnitStateSwitcher
    {
        private readonly List<UnitState> _allStates;
        private UnitState _currentState;        

        public UnitBrain(IUnitBrainContext agentContext)
        {            
            _allStates = new()
            {
                new PatrolState(this, agentContext),
                new ChaseState(this, agentContext),
                new AttackState(this, agentContext),
                new DamageState(this, agentContext),
                new DestroyState(this, agentContext),
                new WaitState(this, agentContext)
            };
        }

        public void Reset()
        {
            SwitchState<PatrolState>();
        }

        public void SwitchState<T>() where T : UnitState
        {
            var state = _allStates.FirstOrDefault(s => s is T);

            _currentState?.OnExit();
            _currentState = state;
            _currentState?.OnEnter();
        }

        public void OnUpdate() => _currentState?.OnUpdate();

        public void Kill() => SwitchState<DestroyState>();

        public void Stop() => SwitchState<WaitState>();
    }
}