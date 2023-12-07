using System;
using SA.Gameplay.Player;
using UnityEngine;

namespace SA.Gameplay.Enemies
{
    [RequireComponent(typeof(SphereCollider))]
    public class LookSensorComponent : MonoBehaviour, ILookSensor
    {
        private SphereCollider _collider;

        public IPlayerTarget Target {get; private set;}

        public event Action<IPlayerTarget> OnDetectTargetEvent;

        private void Awake() 
        {
            _collider = GetComponent<SphereCollider>(); 
            _collider.isTrigger = true;  
        }

        public void Init(float radius) 
        {
            _collider.radius = radius;
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.TryGetComponent(out IPlayerTarget player))
            {
                Target = player;
                OnDetectTargetEvent?.Invoke(player);
            }
        }        
    }
}