using System;
using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/MainConfig", fileName = "MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public LevelSettings[] Levels {get; private set;}

        [Serializable]
        public class LevelSettings
        {
            [field: SerializeField] public MapConfig MapConfig {get; private set;}
            [field: SerializeField] public LevelConfig LevelConfig {get; private set;}
        }
    }
}


