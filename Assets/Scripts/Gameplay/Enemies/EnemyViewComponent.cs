using UnityEngine;

namespace SA.Gameplay.Enemies
{    
    public class EnemyViewComponent : MonoBehaviour, IAnimation
    {
        [SerializeField] private Animator _animator;

        private int _attackAnim;
        private int _speedPrm;

        private void Awake() 
        {
            _attackAnim = Animator.StringToHash("Attack");   
            _speedPrm = Animator.StringToHash("Speed");    
        }

        public void Attack()
        {
            _animator.SetTrigger(_attackAnim);
        }

        public void Move()
        {
            _animator.SetFloat(_speedPrm, 1f);
        }

        public void Idle()
        {
            _animator.SetFloat(_speedPrm, 0f);
        }
    }
}