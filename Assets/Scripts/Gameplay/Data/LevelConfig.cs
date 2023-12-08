using SA.Gameplay.Enemies;
using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve EnemyDensityPerChank {get; private set;}
        [field: SerializeField] public EnemyUnit EnemyPrefab {get; private set;}
        [field: SerializeField, Min(1)] public int MaxUnitCountPerChank {get; private set;} = 20;
    }
}