using UnityEngine;
using Cysharp.Threading.Tasks;
using SA.Gameplay.GameCamera;
using SA.Services;
using SA.Gameplay.UI.HUD;

namespace SA.Gameplay
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [Space, SerializeField] private Transform _startWorld;

        private HUDController _hud;
        private GameProcess _gameProcess;

        private async void Start()
        {
            await InitHUDAsync();            

            _gameProcess = new GameProcess
            (
                _cameraController,
                _startWorld
            );    

            _gameProcess.OnSuccessEvent += Win;
            _gameProcess.OnFailureEvent += Loss;

            _hud.StartScreen();

            await WaitPlayerTapAsync();

            _gameProcess.StartGame();

            _hud.GameplayScreen();
        }

       
        private async UniTask InitHUDAsync()
        {
            await UniTask.WaitUntil(() => FindFirstObjectByType<HUDController>() != null);
            _hud = FindFirstObjectByType<HUDController>();
            _hud.Init();
        }        

        private void Update() => _gameProcess?.OnUpdate();

        private async void Loss()
        {
            _gameProcess.OnSuccessEvent -= Win;
            _gameProcess.OnFailureEvent -= Loss;

            _hud.LoseScreen();

            await WaitPlayerTapAsync();

            RestartLevel();
        }        

        private async void Win()
        {
            _gameProcess.OnSuccessEvent -= Win;
            _gameProcess.OnFailureEvent -= Loss;

            _hud.WinScreen();

            await WaitPlayerTapAsync();            

            // next level..
            var maxLevels = SceneContext.Instance.MainConfig.Levels.Length;
            SceneContext.Instance.PlayerStatsService.SetNextLevel(maxLevels);

            RestartLevel(); 
        }

        private static void RestartLevel()
        {
            SceneContext.Instance.PlayerStatsService.Save();
            SceneContext.Instance.PoolGoService.Clear(); 
            SceneContext.Instance.SceneService.LoadGame();           
        }

        public async UniTask WaitPlayerTapAsync()
        {
            await UniTask.WaitUntil(() => SceneContext.Instance.InputServices.IsTapScreen);
        } 
    }
}
