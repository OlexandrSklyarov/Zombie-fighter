using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA.Gameplay.Vfx
{
    public class VfxItem : MonoBehaviour, IPoolable<VfxItem>
    {
        [field: SerializeField] public VfxType Type {get; private set;}

        [SerializeField] private ParticleSystem _vfx;
        [SerializeField, Min(0.1f)] private float _destroyDelay = 2f;

        private IObjectPool<VfxItem> _pool;


        void IPoolable<VfxItem>.SetPool(IObjectPool<VfxItem> pool)
        {
            _pool = pool;
        }

        public void Init(Vector3 position)
        {
            transform.position = position;
            _vfx.Play();

            ReclaimAsync();
        }

        private async void ReclaimAsync()
        {
            await Awaitable.WaitForSecondsAsync(_destroyDelay);
      
            _pool.Release(this);
        }
    }
}
