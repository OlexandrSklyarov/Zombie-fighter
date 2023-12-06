using UnityEngine;

namespace SA.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        public void GameplayScreen()
        {
            Debug.Log("Gameplay Screen");
        }

        public void LoseScreen()
        {
            Debug.Log("lose screen");
        }

        public void WinScreen()
        {
            Debug.Log("win screen");
        }
    }
}
