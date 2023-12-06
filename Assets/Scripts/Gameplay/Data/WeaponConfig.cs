using System;
using SA.Gameplay.Weapons;
using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/WeaponConfig", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {        
        [field: SerializeField] public FireSettings Settings {get; private set;}
        [field: Space, SerializeField] public WeaponController WeaponPrefab {get; private set;}
        [field: SerializeField] public Projectile ProjectilePrefab{get; private set;}

        [Serializable]
        public class FireSettings
        {
            [field: SerializeField, Min(1f)] public float ProjectilePushForce {get; private set;} = 5f;
            [field: SerializeField, Min(0.01f)] public float FireCooldown {get; private set;} = 0.5f;
            [field: SerializeField, Min(1f)] public float RotationLimit {get; private set;} = 45f;
            [field: SerializeField, Min(1f)] public float RotateSmoothSpeed {get; private set;} = 10f;
            [field: SerializeField, Min(1f)] public float LifeTime {get; private set;} = 3f;
        }
    }
}