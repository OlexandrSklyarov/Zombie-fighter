using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/PopupTextConfig", fileName = "PopupTextConfig")]
    public class PopupTextConfig : ScriptableObject
    {        
        [field: SerializeField, Min(1f)] public float StartVelocity {get; private set;} = 5f;
        [field: SerializeField, Min(1f)] public float VelocityDecayRate {get; private set;} = 10f;
        [field: SerializeField, Min(1f)] public float TimeBeforeFadeStart {get; private set;} = 0.6f;
        [field: SerializeField, Min(1f)] public float FadeSpeed {get; private set;} = 3f;
    }
}