using System;
using AS.Gameplay.Map;
using AS.Services.Input;
using SA.Gameplay.Data;
using UnityEngine;

namespace SA.Gameplay.Player
{
    public class PlayerVehicleController : MonoBehaviour
    {
        public float PathProgress {get; private set;}

        [SerializeField] private VehicleConfig _vehicleConfig;
        [SerializeField] private Transform _weaponOrigin;

        private IMovedPath _path;
        private WeaponController _weapon;
        private bool isCanMove;

        public event Action OnCompletedEvent;
        public event Action OnDestroyEvent;

        public void Init(IInputService input, IMovedPath path, WeaponConfig weaponConfig)
        {
            _path = path;

            input.OnHorizontalMoveEvent += SetWeaponRotate;
            
            _weapon = Instantiate(weaponConfig.WeaponPrefab, _weaponOrigin.position, 
                _weaponOrigin.rotation, _weaponOrigin);

            _weapon.Init(weaponConfig.Settings, weaponConfig.ProjectilePrefab);            

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
