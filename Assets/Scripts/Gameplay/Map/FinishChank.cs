using UnityEngine;

namespace SA.Gameplay.Map
{
    public class FinishChank : MonoBehaviour
    {
        public Vector3 FinishPoint => _finishPoint.position;

        [SerializeField] private Transform _finishPoint;        
    }
}
