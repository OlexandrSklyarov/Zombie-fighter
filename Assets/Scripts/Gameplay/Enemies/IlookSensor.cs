using System;
using SA.Gameplay.Player;

namespace SA.Gameplay.Enemies
{
    public interface ILookSensor
    {
        IPlayerTarget Target {get;}
        event Action<IPlayerTarget> OnDetectTargetEvent;
    }
}