using System;
using AS.Gameplay.Map;
using AS.Services.Input;
using SA.Gameplay.Data;
using SA.Gameplay.Weapons;
using UnityEngine;

namespace SA.Gameplay.Player
{
    public class PlayerVehicleController : MonoBehaviour
    {
        [field: SerializeField] public Transform WeaponOrigin {get; private set;}
        public float PathProgress {get; private set;}

        [SerializeField] private VehicleConfig _vehicleConfig;

        private IMovedPath _path;
        private IShootable _weapon;
        private bool isCanMove;

        public event Action OnCompletedEvent;
        public event Action OnDestroyEvent;

        public void Init(IInputService input, IMovedPath path, IShootable weapon)
        {
            _path = path;

            input.OnHorizontalMoveEvent += SetWeaponRotate;
            
            _weapon = weapon;          

            isCanMove = true;
        }

        public void OnUpdate()
        {
            if (!isCanMove) return;

            Move();

            _weapon.OnUpdate();
        }

        private void SetWeaponRotate(float deltaRotation)
        {
            if (!isCanMove) return;

            _weapon.SetRotation(deltaRotation);            
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards
            (
                transform.position,
                _path.EndPoint,
                _vehicleConfig.Speed * Time.deltaTime
            );

                       
            PathProgress = GetProgress();

            CheckDestination();
        }

        private void CheckDestination()
        {
            if (PathProgress < 1f) return;

            isCanMove = false;

            OnCompletedEvent?.Invoke();
        }

        private float GetProgress()
        {
            var maxDist = (_path.EndPoint - _path.StartPoint).magnitude;
            var curDist = (_path.EndPoint - transform.position).magnitude;            
            return Mathf.InverseLerp(maxDist, 0, curDist);
        }
    }
}
