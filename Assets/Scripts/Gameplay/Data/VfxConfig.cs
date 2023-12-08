using SA.Gameplay.UI;
using SA.Gameplay.Vfx;
using UnityEngine;

namespace SA.Gameplay.Data
{
    [CreateAssetMenu(menuName = "SO/Data/VfxConfig", fileName = "VfxConfig")]
    
    public class VfxConfig : ScriptableObject
    {
        [field: SerializeField] public VfxItem[] Items {get; private set;}         
        [field: Space, SerializeField] public PopupText PopupTextPrefab {get; private set;}         
    }
 }