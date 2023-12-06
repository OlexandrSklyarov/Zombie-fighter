using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA.Gameplay.Enemies
{
    public abstract class EnemyUnit : MonoBehaviour, IPoolable<EnemyUnit>
    {
        [field: SerializeField] public UnitType Type {get; private set;}

        private IObjectPool<EnemyUnit> _pool;


        void IPoolable<EnemyUnit>.SetPool(IObjectPool<EnemyUnit> pool)
        {
            _pool = pool;
        }
    }
}