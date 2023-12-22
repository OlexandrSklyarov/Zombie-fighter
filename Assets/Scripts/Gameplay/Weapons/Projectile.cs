using System;
using SA.Gameplay.GameEntity;
using SA.Gameplay.Health;
using SA.Gameplay.Vfx;
using SA.Gameplay.Weapons;
using SA.Services;
using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IPoolable<Projectile>, IGameEntity
    {
        [field: SerializeField] public ProjectileType Type {get; private set;}

        int IGameEntity.InstanceID => gameObject.GetInstanceID();

        [SerializeField] private VfxType _vfxType;
        [SerializeField, Min(1)] private int _damage = 5;
        [Space, SerializeField] private TrailRenderer _trail;

        private Rigidbody _rb;
        private IObjectPool<Projectile> _pool;
        private Vector3 _pushDirection;
        private float _force;
        private float _autoDestroyTimer;
        private bool _isActive;
        private IUpdateManager _updateManager;
        private bool _isReleased;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Push(Vector3 dir, float force, float lifeTime)
        {                 
            SceneContext.Instance.UpdateManager.Add(this);

            _pushDirection = dir;
            _force = force;
            _autoDestroyTimer = lifeTime;
            
            _isReleased = false;
            _isActive = true;
        }

        void IPoolable<Projectile>.SetPool(IObjectPool<Projectile> pool)
        {
            _pool = pool;
        }

        private void Reclaim()
        {
            if (_isReleased) return;

            SceneContext.Instance.UpdateManager.Remove(this);
            
            ResetVelocity();

            _trail.Clear();
            
            _isReleased = true;
            _isActive = false;
            _pool.Release(this);
        }

        private void ResetVelocity()
        {
            if (_rb.velocity != Vector3.zero) _rb.velocity = Vector3.zero;
            if (_rb.angularVelocity != Vector3.zero) _rb.angularVelocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision other) 
        {
            if (other.gameObject.TryGetComponent(out IDamageble target)) 
            {
                target.ApplyDamage(_damage);
            }   

            PlayVfx();
            
            Reclaim();
        }

        private void PlayVfx()
        {
            SceneContext.Instance.VfxService.Play(transform.position, _vfxType);
        }

        void IGameEntity.OnUpdate()
        {
            if (!_isActive) return;

            var newPos = transform.position + _pushDirection * _force * Time.deltaTime;  
            _rb.MovePosition(newPos);

            _autoDestroyTimer -= Time.deltaTime;

            if (_autoDestroyTimer <= 0f)
            {
                Reclaim();
            }
        }        
    }       
}
