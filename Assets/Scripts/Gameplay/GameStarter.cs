using SA.Gameplay.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using SA.Gameplay.GameCamera;
using SA.Services;

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
            await FindHUDAsync();            

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

       
        private async UniTask FindHUDAsync()
        {
            await UniTask.WaitUntil(() => FindFirstObjectByType<HUDController>() != null);
            _hud = FindFirstObjectByType<HUDController>();
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

            RestartLevel(); // next level..
        }

        private static void RestartLevel()
        {
            SceneContext.Instance.PoolGoService.Clear();
            SceneContext.Instance.SceneService.LoadGame();
        }

        public async UniTask WaitPlayerTapAsync()
        {
            await UniTask.WaitUntil(() => SceneContext.Instance.InputServices.IsTapScreen);
        } 
    }
}
