using System;
using SA.Gameplay.Units;
using SA.Gameplay.Vfx;
using SA.Services;
using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA.Gameplay.Enemies
{
    [RequireComponent(typeof(CapsuleCollider), typeof(HealthComponent))]
    public class EnemyUnit : MonoBehaviour, IPoolable<EnemyUnit>, IDamageble
    {
        [field: SerializeField] public EnemyType Type {get; private set;}

        [SerializeField] private VfxType _vfxType;

        private IObjectPool<EnemyUnit> _pool;
        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        public void Init()
        {
            _health.Restore();
        }

        void IPoolable<EnemyUnit>.SetPool(IObjectPool<EnemyUnit> pool)
        {
            _pool = pool;
        }

        public void ApplyDamage(int damage)
        {
            _health.Value -= damage;

            if (_health.Value <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            PlayVfx();
            _pool.Release(this);
        }

        private void PlayVfx()
        {
            SceneContext.Instance.VfxService.Play(transform.position, _vfxType);
        }

        public void OnUpdate()
        {
        }
    }
}