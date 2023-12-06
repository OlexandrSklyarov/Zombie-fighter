using SA.Gameplay.Data;
using UnityEngine;

namespace SA.Gameplay.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;

        private WeaponConfig.FireSettings _settings;
        private Projectile _projectilePrefab;
        private Quaternion _originWeaponRotation;
        private float _horizontalAngle;
        private Quaternion _targetRotation;

        public void Init(WeaponConfig.FireSettings settings, Projectile projectilePrefab)
        {
            _settings = settings;
            _projectilePrefab = projectilePrefab;
            _originWeaponRotation = transform.localRotation;
        }        

        public void SetRotation(float deltaRotation)
        {
            _horizontalAngle += deltaRotation;
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
        }

        private void Shoot()
        {
            
        }
    }
}
