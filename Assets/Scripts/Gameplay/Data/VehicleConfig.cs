using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/VehicleConfig", fileName = "VehicleConfig")]
    public class VehicleConfig : ScriptableObject
    {        
        [field: SerializeField, Min(1f)] public float Speed {get; private set;} = 5f;
    }
}