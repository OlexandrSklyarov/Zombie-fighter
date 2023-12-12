using System;
using AS.Services.Input;
using SA.Gameplay.Data;
using SA.Gameplay.GameEntity;
using SA.Services.ObjectPool;
using UnityEngine;

namespace SA.Services
{
    public class SceneContext : MonoBehaviour
    {
        public static SceneContext Instance => _instance;
        private static SceneContext _instance;

        public MainConfig MainConfig {get; private set;}
        public ScoreService ScoreService {get; private set;}
        public SceneService SceneService {get; private set;}
        public PlayerStatsService PlayerStatsService {get; private set;}
        public PoolManager PoolGoService {get; private set;}
        public IInputService InputServices { get; private set; }
        public VfxService VfxService { get; private set; }
        public IUpdateManager UpdateManager { get; internal set; }

        private bool _isInit;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        public void Init(MainConfig mainConfig)
        {
            if (_isInit)
            {
                throw new Exception("The initialization has already been done!!!");
            }

            MainConfig = mainConfig;

            ScoreService = new ScoreService(0);
            SceneService = new SceneService();
            PlayerStatsService = new PlayerStatsService();
            InputServices = new TouchInputService();
            PoolGoService = new PoolManager();
            VfxService = new VfxService(MainConfig.VfxConfig, PoolGoService);
            UpdateManager = new GameEntityUpdateManager();

            _isInit = true;
        }
    }
}
