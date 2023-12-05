using SA.Gameplay.Data;
using SA.Services;
using UnityEngine;

namespace SA.Gameplay.Bootstrap
{
    public class AppStartup : MonoBehaviour
    {        
        [SerializeField] private MainConfig _config;

        private void Start()
        {
            SceneContext.Instance.Init(_config);

            SceneContext.Instance.SceneService.LoadGame();
        }      
    }
}
