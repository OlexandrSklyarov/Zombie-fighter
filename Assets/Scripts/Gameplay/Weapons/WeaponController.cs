using SA.Gameplay.Data;
using SA.Gameplay.GameEntity;
using SA.Services.ObjectPool;
using UnityEngine;

namespace SA.Gameplay.Weapons
{
    public class WeaponController : MonoBehaviour, IShootable
    {
        [SerializeField] private Transform _firePoint;

        private WeaponConfig.FireSettings _settings;
        private Quaternion _originWeaponRotation;
        private Quaternion _targetRotation;
        private PoolManager _poolService;
        private Projectile _projectilePrefab;
        private float _horizontalAngle;
        private float _nextShootTime;
        private bool _isCanShoot;

        public void Init(WeaponConfig.FireSettings settings, 
            Projectile projectilePrefab, 
            PoolManager poolService)
        {
            _settings = settings;
            _projectilePrefab = projectilePrefab;
            _originWeaponRotation = transform.localRotation;
            _poolService = poolService;
        }        

        public void SetRotation(float deltaRotation)
        {
            _horizontalAngle += deltaRotation * _settings.Sensitivity;
            _horizontalAngle = Mathf.Clamp(_horizontalAngle, -_settings.RotationLimit, _settings.RotationLimit);
            _targetRotation = _originWeaponRotation * Quaternion.AngleAxis(_horizontalAngle, Vector3.up);            
        }

        public void OnUpdate()
        {
            transform.localRotation = Quaternion.Slerp
            (
                transform.localRotation,
                _targetRotation,
                Time.deltaTime * _settings.RotateSmoothSpeed
            );

            Shoot();
        }

        private void Shoot()
        {
            if (!_isCanShoot) return;
            if (Time.time < _nextShootTime) return;

            var projectile = _poolService.GetProjectile(_projectilePrefab);
            projectile.transform.SetPositionAndRotation(_firePoint.transform.position, _firePoint.transform.rotation);
            projectile.Push(_firePoint.transform.forward, _settings.ProjectilePushForce, _settings.LifeTime);

            _nextShootTime = Time.time + _settings.FireCooldown;
        }

        public void SwitchShoot()
        {
            _isCanShoot = !_isCanShoot;
        }
    }
}
