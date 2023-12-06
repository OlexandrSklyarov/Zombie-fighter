using SA.Gameplay.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using SA.Gameplay.GameCamera;

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

            await _gameProcess.WaitPlayerTapAsync();

            _hud.GameplayScreen();
        }
        
        private async UniTask FindHUDAsync()
        {
            await UniTask.WaitUntil(() => FindObjectOfType<HUDController>() != null);
            _hud = FindObjectOfType<HUDController>();
        }        

        private void Update() => _gameProcess?.OnUpdate();

        private void Loss()
        {
            _gameProcess.OnSuccessEvent -= Win;
            _gameProcess.OnFailureEvent -= Loss;

            _hud.LoseScreen();
        }

        private void Win()
        {
            _gameProcess.OnSuccessEvent -= Win;
            _gameProcess.OnFailureEvent -= Loss;

            _hud.WinScreen();
        }
    }
}
