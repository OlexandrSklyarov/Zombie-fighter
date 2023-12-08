using UnityEngine;

namespace SA.Gameplay.Enemies
{    
    public class EnemyViewComponent : MonoBehaviour, IAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField, Min(0.01f)] private float _attackTime = 1f;
        [SerializeField, Min(0.01f)] private float _damageTime = 1f;

        private int _attackAnim;
        private int _speedPrm;
        private int _damageAnim;

        private void Awake() 
        {
            _attackAnim = Animator.StringToHash("Attack");   
            _speedPrm = Animator.StringToHash("Speed");    
            _damageAnim = Animator.StringToHash("Damage");    
        }

        public float Attack()
        {
            _animator.SetTrigger(_attackAnim);
            return _attackTime;
        }

        public void Move()
        {
            _animator.SetFloat(_speedPrm, 1f);
        }

        public void Idle()
        {
            _animator.SetFloat(_speedPrm, 0f);
        }

        public float Damage()
        {
            _animator.SetTrigger(_damageAnim);
            return _damageTime;
        }
    }
}