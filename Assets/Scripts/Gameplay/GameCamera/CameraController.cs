using System;
using Cinemachine;
using UnityEngine;

namespace SA.Gameplay.GameCamera
{
    [Serializable]
    public class CameraController
    {
        private enum CameraType {START, MAIN}

        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _startCamera;


        public void Init(Transform target)
        {
            _mainCamera.Follow = target;
            _mainCamera.LookAt = target;

            _startCamera.Follow = target;
            _startCamera.LookAt = target;
        }

        public void ActiveStartCamera() => SetCameraPriority(CameraType.START);

        public void ActiveFollowCamera() => SetCameraPriority(CameraType.MAIN);

        private void SetCameraPriority(CameraType type)
        {
            _mainCamera.Priority = (type == CameraType.MAIN) ? 10 : 0;
            _startCamera.Priority = (type == CameraType.START) ? 10 : 0;
        }
    }
}
