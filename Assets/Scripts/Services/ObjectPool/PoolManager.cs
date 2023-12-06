using System;
using System.Collections.Generic;
using SA.Gameplay.Enemies;
using SA.Gameplay.Vfx;
using SA.Gameplay.Weapons;
using UnityEngine;

namespace SA.Services.ObjectPool
{
    public class PoolManager
    {
        private readonly Dictionary<EnemyType, BaseGOPool<EnemyUnit>> _unitPools = new();
        private readonly Dictionary<ProjectileType, BaseGOPool<Projectile>> _projectilePools = new();
        private readonly Dictionary<VfxType, BaseGOPool<VfxItem>> _vfxPools = new();


        public EnemyUnit GetUnit(EnemyUnit prefab, int startPoolCount = 32, int maxPoolAmount = 32)
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
        
        public Projectile GetProjectile(Projectile prefab, int startPoolCount = 32, int maxPoolAmount = 32)
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


        public VfxItem GetVFX(VfxItem prefab, int startPoolCount = 32, int maxPoolAmount = 32)
        {
            var type = prefab.Type;

            if (_vfxPools.TryGetValue(type, out var pool))
            {
                return pool.Get();
            }

            _vfxPools.Add
            (
                prefab.Type,
                new BaseGOPool<VfxItem>(prefab, startPoolCount, maxPoolAmount, $"POOL - [{type}]")                        
            );

            return _vfxPools[type].Get();
        }
    }
}