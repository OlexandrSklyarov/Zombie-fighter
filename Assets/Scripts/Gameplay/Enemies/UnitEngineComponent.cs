using UnityEngine;

namespace SA.Gameplay.Enemies
{
    [RequireComponent(typeof(CharacterController))]
    public class UnitEngineComponent : MonoBehaviour, IMovable
    {
        private float _speed;

        private CharacterController _cc;

        private void Awake() 
        {
            _cc = GetComponent<CharacterController>();  
        }

        public void Init(float speed) => _speed = speed;

        public void Move(Vector3 targetPos)
        {
            var dir = (targetPos - transform.position).normalized;
            _cc.Move(dir * _speed * Time.deltaTime);

            transform.rotation = Quaternion.AngleAxis
            (
                Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg,
                Vector3.up
            );
        }
    }
}