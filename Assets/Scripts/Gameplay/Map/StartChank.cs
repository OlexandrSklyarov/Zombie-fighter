using UnityEngine;

namespace SA.Gameplay.Map
{
    public class StartChank : MonoBehaviour
    {
        public Vector3 StartPoint => _startPoint.position;

        [SerializeField] private Transform _startPoint;
    }
}
