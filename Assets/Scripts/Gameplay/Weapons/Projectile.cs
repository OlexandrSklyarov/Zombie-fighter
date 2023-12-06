using System;
using SA.Gameplay.Weapons;
using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA
{
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        private IObjectPool<Projectile> _pool;
        private Vector3 _pushDirection;
        private float _force;
        private float _autoDestroyTimer;
        private bool _isActive;

        [field: SerializeField] public ProjectileType Type {get; private set;}

        public void Push(Vector3 dir, float force, float lifeTime)
        {
            _pushDirection = dir;
            _force = force;
            _autoDestroyTimer = lifeTime;
            _isActive = true;
        }

        void IPoolable<Projectile>.SetPool(IObjectPool<Projectile> pool)
        {
            _pool = pool;
        }

        private void Reclaim()
        {
            _isActive = false;
            _pool.Release(this);
        }

        private void Update() 
        {
            if (!_isActive) return;

            transform.position += _pushDirection * _force * Time.deltaTime;  

            _autoDestroyTimer -= Time.deltaTime;

            if (_autoDestroyTimer <= 0f)
            {
                DestroyProjectile();
            }  
        }

        private void DestroyProjectile()
        {
            Debug.Log("VFX");
            Reclaim();
        }

        internal void Push(Vector3 forward, float projectilePushForce, object lifeTime)
        {
            throw new NotImplementedException();
        }
    }
}
