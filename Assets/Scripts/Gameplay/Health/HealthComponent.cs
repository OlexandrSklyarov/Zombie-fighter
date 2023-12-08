using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;

namespace SA.Gameplay.Health
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

        public bool IsAlive => _currentHP > 0;

        [SerializeField] private int _startHP = 10;
        [SerializeField, Min(0.01f)] private float _changeValueAnimationDuration = 0.5f;
        [SerializeField] private Image _fastBar;
        [SerializeField] private Image _slowBar;  
        [SerializeField] private GameObject _hpBarRoot;  

        private Tween _barTween;     

        private int _currentHP;

        public void Restore() => _currentHP = _startHP;        

        private void ChangeView()
        {
            var value = (float)_currentHP / _startHP;

            _barTween.Complete();

            _barTween = _fastBar.transform.DOScaleX(value, _changeValueAnimationDuration)
                .OnComplete(() => _slowBar.transform.DOScaleX(value, _changeValueAnimationDuration));
        }

        private void OnDisable() 
        {
            _barTween?.Kill();
        }

        public void Hide() => _hpBarRoot.SetActive(false);
    }
}