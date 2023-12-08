using UnityEngine;

namespace SA.Gameplay.Map
{
    [RequireComponent(typeof(BoxCollider))]
    public class FireSwitcher : MonoBehaviour
    {
        private void Awake() 
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}