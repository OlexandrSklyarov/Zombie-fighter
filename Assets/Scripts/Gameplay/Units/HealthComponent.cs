using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace SA.Gameplay.Units
{
    public class HealthComponent : MonoBehaviour
    {
        public int Value 
        { 
            get => _currentHP;
            set
            {
                _currentHP = value;
                ChangeView();
            }
        }

        [SerializeField] private int _startHP = 10;
        [SerializeField] private Image _fastBar;
        [SerializeField] private Image _slowBar;       

        private int _currentHP;

        public void Restore() => _currentHP = _startHP;

        private void ChangeView()
        {
            var value = (float)_currentHP / _startHP;

            _fastBar.transform.DOScaleX(value, 1f)
                .OnComplete(() => _slowBar.transform.DOScaleX(value, 1f));
        }
    }
}