using System.Collections.Generic;
using SA.Gameplay.Enemies;
using SA.Gameplay.Weapons;

namespace SA.Services.ObjectPool
{
    public class PoolManager
    {
        private readonly Dictionary<UnitType, BaseGOPool<EnemyUnit>> _unitPools = new();
        private readonly Dictionary<ProjectileType, BaseGOPool<Projectile>> _projectilePools = new();


        public EnemyUnit GetUnit(EnemyUnit prefab, int startPoolCount = 10, int maxPoolAmount = 32)
        {
            var type = prefab.Type;

            if (_unitPools.TryGetValue(type, out var pool))
            {
                return pool.Get();
            }

            _unitPools.Add
            (
                prefab.Type,
                new BaseGOPool<EnemyUnit>(prefab, startPoolCount, maxPoolAmount, $"POOL - [{type}]")                        
            );

            return _unitPools[type].Get();
        }  
        
        public Projectile GetProjectile(Projectile prefab, int startPoolCount = 10, int maxPoolAmount = 32)
        {
            var type = prefab.Type;

            if (_projectilePools.TryGetValue(type, out var pool))
            {
                return pool.Get();
            }

            _projectilePools.Add
            (
                prefab.Type,
                new BaseGOPool<Projectile>(prefab, startPoolCount, maxPoolAmount, $"POOL - [{type}]")                        
            );

            return _projectilePools[type].Get();
        }    
    }
}