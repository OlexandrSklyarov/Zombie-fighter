using UnityEngine;

namespace SA.Gameplay.Enemies
{
    public interface IMovable
    {
        void Move(Vector3 targetPos);
    }
}