using System.Linq;
using SA.Gameplay.Data;
using SA.Gameplay.Vfx;
using SA.Services.ObjectPool;
using UnityEngine;

namespace SA.Services
{
    public class VfxService
    {
        private VfxConfig _config;
        private PoolManager _poolManager;

        public VfxService(VfxConfig config, PoolManager poolManager)
        {
            _config = config;
            _poolManager = poolManager;
        }

        public void Play(Vector3 position, VfxType vfxType)
        {
            var prefab = _config.Items.FirstOrDefault(x => x.Type == vfxType);

            if (prefab == null)
            {
                Debug.LogError("VFX not fount!!!");
                return;
            }

            var vfx = _poolManager.GetVFX(prefab);
            vfx.Init(position);
        }

        public void CreatePopupText(Vector3 position, string str, Color color)
        {
            var vfx = _poolManager.GetPopupText(_config.PopupTextPrefab);
            vfx.transform.position = position;
            vfx.Init(str, color);
        }
    }
}
