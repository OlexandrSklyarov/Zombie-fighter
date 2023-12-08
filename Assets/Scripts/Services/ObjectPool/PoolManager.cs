using System.Collections.Generic;
using SA.Gameplay.Enemies;
using SA.Gameplay.UI;
using SA.Gameplay.Vfx;
using SA.Gameplay.Weapons;

namespace SA.Services.ObjectPool
{
    public class PoolManager
    {
        private readonly Dictionary<EnemyType, BaseGOPool<EnemyUnit>> _unitPools = new();
        private readonly Dictionary<ProjectileType, BaseGOPool<Projectile>> _projectilePools = new();
        private readonly Dictionary<VfxType, BaseGOPool<VfxItem>> _vfxPools = new();
        private readonly Dictionary<int, BaseGOPool<PopupText>> _popupTextPools = new();

        public void Clear()
        {
            foreach(var pool in _unitPools) pool.Value.Clear();
            foreach(var pool in _projectilePools) pool.Value.Clear();
            foreach(var pool in _vfxPools) pool.Value.Clear();
            foreach(var pool in _popupTextPools) pool.Value.Clear();
        }

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


        public VfxItem GetVFX(VfxItem prefab, int startPoolCount = 10, int maxPoolAmount = 32)
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

        public PopupText GetPopupText(PopupText prefab, int startPoolCount = 10, int maxPoolAmount = 32)
        {
            var id = prefab.GetInstanceID();

            if (_popupTextPools.TryGetValue(id, out var pool))
            {
                return pool.Get();
            }

            _popupTextPools.Add
            (
                id,
                new BaseGOPool<PopupText>(prefab, startPoolCount, maxPoolAmount, $"POOL - [{prefab.name}]")                        
            );

            return _popupTextPools[id].Get();
        }
    }
}