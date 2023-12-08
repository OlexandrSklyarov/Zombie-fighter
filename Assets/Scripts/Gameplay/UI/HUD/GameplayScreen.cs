using SA.Services;
using TMPro;
using UnityEngine;

namespace SA.Gameplay.UI.HUD
{
    public class GameplayScreen : HudScreen
    {
        [SerializeField] private TextMeshProUGUI _pointsText;
        
        public void Init()
        {
            SetPoints(SceneContext.Instance.PlayerStatsService.CurrentPoints);
        }

        private void SetPoints(int currentPoints)
        {
            _pointsText.text = $"{currentPoints}";
        }

        private void OnEnable() 
        {
            SceneContext.Instance.PlayerStatsService.ChangePointsEvent += SetPoints;  
        }

        private void OnDisable() 
        {
            SceneContext.Instance.PlayerStatsService.ChangePointsEvent -= SetPoints;  
        }
    }
}