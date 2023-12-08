using SA.Gameplay.Map;
using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/MapConfig", fileName = "MapConfig")]
    public class MapConfig : ScriptableObject
    {
        [field: SerializeField] public StartChank StartChankPrefab {get; private set;}
        [field: SerializeField] public FinishChank FinishChankPrefab {get; private set;}
        [field: SerializeField] public MapChank MapChankPrefab {get; private set;}
        [field: SerializeField, Min(1f)] public float ChankOffset {get; private set;} = 105f;
        [field: SerializeField, Min(1)] public int ChankCount {get; private set;} = 5;
    }
}

