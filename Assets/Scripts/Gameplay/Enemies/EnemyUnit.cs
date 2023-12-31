using System;
using SA.Gameplay.Data;
using SA.Gameplay.Enemies.FSM;
using SA.Gameplay.Health;
using SA.Gameplay.Vfx;
using SA.Services;
using SA.Services.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace SA.Gameplay.Enemies
{
    [RequireComponent(typeof(HealthComponent), typeof(UnitEngineComponent), typeof(EnemyViewComponent))]
    public class EnemyUnit : MonoBehaviour, IPoolable<EnemyUnit>, IDamageble, IUnitBrainContext
    {
        [field: SerializeField] public EnemyType Type {get; private set;}    
        public int KillCost => _config.KillCost;    

        IMovable IUnitBrainContext.Engine => _engine;
        IAnimation IUnitBrainContext.Animator => _view;
        ILookSensor IUnitBrainContext.Sensor => _sensor;
        Transform IUnitBrainContext.MyTransform => transform;
        float IUnitBrainContext.AttackDistance => _config.AttackDistance;
        int IUnitBrainContext.Damage => _config.AttackDamage;

        [SerializeField] private VfxType _vfxType;
        [SerializeField] private UnitConfig _config;
        [SerializeField] private LookSensorComponent _sensor;

        private IObjectPool<EnemyUnit> _pool;
        private HealthComponent _health;
        private EnemyViewComponent _view;
        private UnitEngineComponent _engine;
        private UnitBrain _brain;

        private bool _isActive;

        public event Action<EnemyUnit> DestroyEvent;
        public event Action DamageEvent;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _view = GetComponent<EnemyViewComponent>();
            _engine = GetComponent<UnitEngineComponent>();            

            _brain = new UnitBrain(this);
        }

        public void Init()
        {
            _engine.Init(_config.Speed);
            _sensor.Init(_config.DetectRadius);
            _health.Restore();
            _brain.Reset();

            _isActive = true;
        }

        public void OnStop()
        {
            if (!_health.IsAlive) return;
            
            _brain?.Stop();
        }        

        public void ApplyDamage(int damage)
        {
            var pos = transform.position + Vector3.up * 4f + Vector3.right * UnityEngine.Random.Range(-2f, 2f);
            SceneContext.Instance.VfxService.CreatePopupText(pos, $"{damage}", Color.red);

            DamageEvent?.Invoke();
            
            _health.Value -= damage;            

            if (!_health.IsAlive)
            {
                DestroyEvent?.Invoke(this);
                Death();
            }
        }

        private void Death()
        {
            if (!_isActive) return;

            _isActive = false;
            PlayVfx();
            _brain.Kill();            

            _pool.Release(this);
        }

        private void PlayVfx()
        {
            SceneContext.Instance.VfxService.Play(transform.position, _vfxType);
        }

        public void OnUpdate()
        {
            if (!_isActive) return;

            _brain?.OnUpdate();
        }

        void IUnitBrainContext.AutoDestroy()
        {
            Death();
        }  

        void IPoolable<EnemyUnit>.SetPool(IObjectPool<EnemyUnit> pool)
        {
            _pool = pool;
        }      
    }
}