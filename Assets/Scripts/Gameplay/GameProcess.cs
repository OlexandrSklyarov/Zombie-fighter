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
        private int PlayerVehicleID => SceneContext.Instance.PlayerStatsService.CurrentVehicle;
        private int PlayerWeaponID => SceneContext.Instance.PlayerStatsService.CurrentWeapon;
        private MapConfig MapConfig => SceneContext.Instance.MainConfig.Levels[CurrentLevel].MapConfig;
        private LevelConfig LevelConfig => SceneContext.Instance.MainConfig.Levels[CurrentLevel].LevelConfig;

        private readonly IInputService _inputService;
        private readonly PlayerVehicleController _playerVehicleController;
        private readonly CameraController _cameraController;
        private readonly MapController _mapController;
        private EnemyController _enemyController;
        private bool _isGameRunning;

        public event Action OnSuccessEvent;
        public event Action OnFailureEvent;

        public GameProcess(CameraController cameraController, Transform _startWorld)
        {
            _cameraController = cameraController;
            
            _inputService = SceneContext.Instance.InputServices;

            _mapController = new MapController(_startWorld, MapConfig);  
            _mapController.Generate(); 

            _playerVehicleController = CreateVehicle();
            _playerVehicleController.OnCompletedEvent += OnSuccess;
            _playerVehicleController.OnDestroyEvent += OnFailure;

            _cameraController.Init(_playerVehicleController.transform);
            _cameraController.ActiveStartCamera();

            _enemyController = new EnemyController(_mapController, LevelConfig);
            _enemyController.GenerateEnemies();
        }    

        private PlayerVehicleController CreateVehicle()
        {
            var prefab = SceneContext.Instance.MainConfig.VehiclePrefabs[PlayerVehicleID];
            var vehicle = GameObject.Instantiate(prefab, _mapController.StartPoint, Quaternion.identity);

            var weaponConfig = SceneContext.Instance.MainConfig.WeaponConfigs[PlayerWeaponID];            
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
            if (!_isGameRunning) return;

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

        public void StartGame() 
        {
           _isGameRunning = true;
           _cameraController.ActiveFollowCamera();
        } 

        private void StopGame() 
        {
            _enemyController?.StopEnemies();
            _isGameRunning = false;
        } 
    }
}