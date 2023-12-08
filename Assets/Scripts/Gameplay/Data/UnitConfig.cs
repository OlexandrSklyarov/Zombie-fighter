using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/UnitConfig", fileName = "UnitConfig")]
    public class UnitConfig : ScriptableObject
    {        
        [field: SerializeField, Min(1f)] public float AttackDistance {get; private set;} = 2f;
        [field: SerializeField, Min(1f)] public float DetectRadius {get; private set;} = 30f;
        [field: SerializeField, Min(1f)] public float Speed {get; private set;} = 5f;
        [field: SerializeField, Min(1)] public int AttackDamage {get; private set;} = 5;
        [field: SerializeField, Min(1)] public int KillCost {get; private set;} = 5;
    }
}