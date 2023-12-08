using UnityEngine;

namespace SA.Gameplay.Map
{
    public class MapChank : MonoBehaviour
    {
        [field: SerializeField] public Transform Center {get; private set;}
        [field: SerializeField] public Vector2 Bounce {get; private set;} = Vector2.one;

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Center.position, new Vector3(Bounce.x, 0f, Bounce.y));
        }
    }
}
