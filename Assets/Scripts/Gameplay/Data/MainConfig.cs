using System;
using SA.Gameplay.Player;
using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/MainConfig", fileName = "MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public LevelSettings[] Levels {get; private set;}
        [field: Space, SerializeField] public PlayerVehicleController[] VehiclePrefabs {get; private set;}
        [field: Space, SerializeField] public WeaponConfig[] WeaponConfigs {get; private set;}

        [Serializable]
        public class LevelSettings
        {
            [field: SerializeField] public MapConfig MapConfig {get; private set;}
            [field: SerializeField] public LevelConfig LevelConfig {get; private set;}
        }
    }
}


