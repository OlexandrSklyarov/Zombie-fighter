using UnityEngine;

namespace SA.Gameplay.Enemies.FSM
{
    public interface IUnitBrainContext
    {        
        IMovable Engine {get;}
        IAnimation Animator {get;}
        ILookSensor Sensor {get;}
        Transform MyTransform {get;}
        float AttackDistance {get;}
        int Damage {get;}

        void AutoDestroy();
    }
}