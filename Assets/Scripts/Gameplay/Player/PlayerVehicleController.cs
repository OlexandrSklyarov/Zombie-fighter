using System;
using AS.Gameplay.Map;
using AS.Services.Input;
using SA.Gameplay.Data;
using SA.Gameplay.Health;
using SA.Gameplay.Map;
using SA.Gameplay.Weapons;
using UnityEngine;

namespace SA.Gameplay.Player
{
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerVehicleController : MonoBehaviour, IPlayerTarget
    {
        [field: SerializeField] public Transform WeaponOrigin {get; private set;}

        Vector3 IPlayerTarget.Position => _targetPoint.position;
        bool IPlayerTarget.IsAlive => _health.IsAlive;
        public float PathProgress {get; private set;}

        [SerializeField] private VehicleConfig _vehicleConfig;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private MeshRenderer[] _parts;

        private IMovedPath _path;
        private IShootable _weapon;
        private bool isCanMove;
        private HealthComponent _health;

        public event Action OnCompletedEvent;
        public event Action OnDestroyEvent;

        private void Awake() 
        {
            _health = GetComponent<HealthComponent>();              
        }

        public void Init(IInputService input, IMovedPath path, IShootable weapon)
        {
            _path = path;            
            _weapon = weapon;   
            _health.Restore(); 

            _parts = GetComponentsInChildren<MeshRenderer>();  

            input.OnHorizontalMoveEvent += SetWeaponRotate;

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

        void IPlayerTarget.ApplyDamage(int damage)
        {
            _health.Value -= damage;

            if (!_health.IsAlive)
            {
                Death();
            }
        }

        private void Death()
        {
            OnDestroyEvent?.Invoke();

            _health.Hide();

            Array.ForEach(_parts, p =>
            {
                p.materials[0].color = Color.black;
                p.gameObject.AddComponent<BoxCollider>();

                var rb = p.gameObject.AddComponent<Rigidbody>();
                rb.AddExplosionForce(500f, transform.position, 100f);
            });
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.GetComponent<FireSwitcher>())
            {
                _weapon.SwitchShoot();
            }
        }
    }
}
