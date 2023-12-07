using UnityEngine;

namespace SA.Gameplay.Player
{
    public interface IPlayerTarget
    {
        Vector3 Position {get;}
        bool IsAlive {get;}
        void ApplyDamage(int damage);
    }
}