using SA.Gameplay.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using SA.Services;
using SA.Gameplay.Data;
using SA.Gameplay.Map;

namespace SA.Gameplay
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private Transform _world;

        private HUDController _hud;
        private GameProcess _gameProcess;

        private async void Start()
        {
            await FindHUD();    

            var mapController = new MapController(_world.position, GetMapConfig());  
            mapController.Generate(); 

            _gameProcess = new GameProcess
            (

            );    
        }

        private MapConfig GetMapConfig()
        {
            var context = SceneContext.Instance;
            var level = context.PlayerStatsService.CurrentLevel;
            return context.MainConfig.Levels[level].MapConfig;
        }

        private async UniTask FindHUD()
        {
            await UniTask.WaitUntil(() => FindObjectOfType<HUDController>() != null);
            _hud = FindObjectOfType<HUDController>();
        }        
    }
}
