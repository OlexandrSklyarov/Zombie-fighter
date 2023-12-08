using UnityEngine;

namespace SA.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _gameplayScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _loseScreen;

        public void GameplayScreen()
        {
            HidAll();
            SetActiveScreen(_gameplayScreen, true);
        }

        public void LoseScreen()
        {
            HidAll();
            SetActiveScreen(_loseScreen, true);
        }

        public void WinScreen()
        {
            HidAll();
            SetActiveScreen(_winScreen, true);
        }

        public void StartScreen()
        {
            HidAll();
            SetActiveScreen(_startScreen, true);
        }

        private void HidAll()
        {
            SetActiveScreen(_startScreen, false);
            SetActiveScreen(_gameplayScreen, false);
            SetActiveScreen(_winScreen, false);
            SetActiveScreen(_loseScreen, false);
        }

        public void SetActiveScreen(GameObject screen, bool isActive)
        {
            screen.SetActive(isActive);
        }
    }
}
