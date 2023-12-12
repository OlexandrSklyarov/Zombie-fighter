using SA.Gameplay.GameEntity;
using SA.Services;
using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA.Gameplay.Vfx
{
    public class VfxItem : MonoBehaviour, IPoolable<VfxItem>, IGameEntity
    {
        [field: SerializeField] public VfxType Type {get; private set;}

        int IGameEntity.InstanceID => gameObject.GetInstanceID();

        [SerializeField] private ParticleSystem _vfx;
        [SerializeField, Min(0.1f)] private float _destroyDelay = 2f;

        private IObjectPool<VfxItem> _pool;
        private float _timer;
        private bool _isReleased;


        void IPoolable<VfxItem>.SetPool(IObjectPool<VfxItem> pool)
        {
            _pool = pool;
        }

        public void Init(Vector3 position)
        {
            transform.position = position;
            _vfx.Play();

            _timer = _destroyDelay;
            _isReleased = false;

            SceneContext.Instance.UpdateManager.Add(this);
        }

        private void Reclaim()
        {
            SceneContext.Instance.UpdateManager.Remove(this);
            _isReleased = true;
            _pool.Release(this);
        }

        void IGameEntity.OnUpdate()
        {
            if (_isReleased) return;
            
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                Reclaim();
            }
        }
    }
}
