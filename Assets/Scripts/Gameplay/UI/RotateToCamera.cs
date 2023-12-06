using UnityEngine;

namespace SA.Gameplay.UI
{
    public class RotateToCamera : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private Transform _camTransform;

        void Awake()
        {
            var cam = Camera.main;

            _camTransform = cam.transform;
            _canvas.worldCamera = cam;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camTransform);
        }
    }
}
