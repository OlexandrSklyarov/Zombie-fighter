using UnityEngine;
using UnityEngine.Pool;

namespace SA.Services.ObjectPool
{
    public interface IPoolable<T> where T : MonoBehaviour
    {
        void SetPool(IObjectPool<T> pool);
    }
}