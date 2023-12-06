using System;
using AS.Services.Input;
using Cysharp.Threading.Tasks;
using SA.Gameplay.Data;
using SA.Gameplay.Enemies;
using SA.Gameplay.GameCamera;
using SA.Gameplay.Map;
using SA.Gameplay.Player;
using SA.Gameplay.Weapons;
using SA.Services;
using UnityEngine;

namespace SA.Gameplay
{
    public class GameProcess
    {
        private int CurrentLevel => SceneContext.Instance.PlayerStatsService.CurrentLevel;
        private int PlayerVehicle => SceneContext.Instance.PlayerStatsService.CurrentVehicle;
        private int PlayerWeapon => SceneContext.Instance.PlayerStatsService.CurrentWeapon;
        private MapConfig MapConfig => SceneContext.Instance.MainConfig.Levels[CurrentLevel].MapConfig;
        private LevelConfig LevelConfig => SceneContext.Instance.MainConfig.Levels[CurrentLevel].LevelConfig;

        private readonly IInputService _inputService;
        private readonly PlayerVehicleController _playerVehicleController;
        private readonly CameraController _cameraController;
        private readonly MapController _mapController;
        private bool _isGameStarted;
        private EnemyController _enemyController;

        public event Action OnSuccessEvent;
        public event Action OnFailureEvent;

        public GameProcess(CameraController cameraController, Transform _startWorld)
        {
            _cameraController = cameraController;
            
            _mapController = new MapController(_startWorld, MapConfig);  
            _mapController.Generate(); 

            _inputService = SceneContext.Instance.InputServices;
            _inputService.OnTapEvent += () => StartGame();

            _playerVehicleController = CreateVehicle();
            _playerVehicleController.OnCompletedEvent += OnSuccess;
            _playerVehicleController.OnDestroyEvent += OnFailure;

            _cameraController.Init(_playerVehicleController.transform);
            _cameraController.ActiveStartCamera();

            _enemyController = new EnemyController(_mapController, LevelConfig);
            _enemyController.GenerateEnemies();
        }      

        public async UniTask WaitPlayerTapAsync()
        {
            await UniTask.WaitUntil(() => _isGameStarted);

            _cameraController.ActiveFollowCamera();

            Debug.Log("Player tap...");
        } 

        private PlayerVehicleController CreateVehicle()
        {
            var prefab = SceneContext.Instance.MainConfig.VehiclePrefabs[PlayerVehicle];
            var vehicle = GameObject.Instantiate(prefab, _mapController.StartPoint, Quaternion.identity);

            var weaponConfig = SceneContext.Instance.MainConfig.WeaponConfigs[PlayerWeapon];            
            var weapon = GetWeapon(weaponConfig, vehicle.WeaponOrigin);
            vehicle.Init(_inputService, _mapController, weapon);

            return vehicle;
        }

        private IShootable GetWeapon(WeaponConfig weaponConfig, Transform weaponOrigin)
        {
            var weapon = GameObject.Instantiate(weaponConfig.WeaponPrefab, weaponOrigin.position, 
                weaponOrigin.rotation, weaponOrigin);

            weapon.Init
            (
                weaponConfig.Settings, 
                weaponConfig.ProjectilePrefab,
                SceneContext.Instance.PoolGoService);  

            return weapon;
        }

        public void OnUpdate()
        {
            if (!_isGameStarted) return;

            _playerVehicleController.OnUpdate();
            _enemyController?.OnUpdate();
        }
       
        private void OnFailure()
        {
            StopGame();

            _playerVehicleController.OnCompletedEvent -= OnSuccess;
            _playerVehicleController.OnDestroyEvent -= OnFailure;

            OnFailureEvent?.Invoke();
        }

        private void OnSuccess()
        {
            StopGame();

            _playerVehicleController.OnCompletedEvent -= OnSuccess;
            _playerVehicleController.OnDestroyEvent -= OnFailure;

            OnSuccessEvent?.Invoke();
        }        

        private void StartGame() => _isGameStarted = true;

        private void StopGame() => _isGameStarted = false;
    }
}