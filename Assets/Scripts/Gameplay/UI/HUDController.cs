using System;
using System.Linq;
using UnityEngine;

namespace SA.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private HudScreen[] _screens;

        private void Awake() 
        {
            _screens = GetComponentsInChildren<HudScreen>(true);
        }

        public void Init() => GetScreen<GameplayScreen>().Init();

        public void GameplayScreen()
        {
            HidAll();
            SetActiveScreen(GetScreen<GameplayScreen>(), true);
        }

        public void LoseScreen()
        {
            HidAll();
            SetActiveScreen(GetScreen<LoseScreen>(), true);
        }

        public void WinScreen()
        {
            HidAll();
            SetActiveScreen(GetScreen<WinScreen>(), true);
        }

        public void StartScreen()
        {
            HidAll();
            SetActiveScreen(GetScreen<StartScreen>(), true);
        }

        private void HidAll()
        {
            Array.ForEach(_screens, x => SetActiveScreen(x, false));
        }

        public void SetActiveScreen(HudScreen screen, bool isActive)
        {
            screen.gameObject.SetActive(isActive);
        }       

        private T GetScreen<T>() where T : HudScreen
        {
            return (T)_screens.First(x => x is T);
        } 
    }
}
