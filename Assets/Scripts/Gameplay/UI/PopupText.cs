using System;
using SA.Gameplay.Data;
using SA.Gameplay.GameEntity;
using SA.Services;
using SA.Services.ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace SA.Gameplay.UI
{
    public class PopupText : MonoBehaviour, IPoolable<PopupText>, IGameEntity
    {
        int IGameEntity.InstanceID => gameObject.GetInstanceID();

        [SerializeField] private PopupTextConfig _config;
        [SerializeField] private TextMeshProUGUI _content;
        
        private IObjectPool<PopupText> _pool;
        private bool _isCanMove;
        private float _timer;
        private Vector3 _currentVelocity;
        private float _textAlpha;
        private Color _startColor;


        public void Init(string msg, Color color)
        {
            color.a = 1f;
            _content.color = color;
            _content.text = msg;

            _startColor = color;
            _timer = 0f;
            _textAlpha = 1f;
            _currentVelocity = Vector3.up * _config.StartVelocity;            
            
            _isCanMove = true;

            SceneContext.Instance.UpdateManager.Add(this);
        }       

        private void Move()
        {
            _currentVelocity.y -= Time.deltaTime * _config.VelocityDecayRate;
            transform.Translate(_currentVelocity * Time.deltaTime);

            _timer += Time.deltaTime;

            if (_timer > _config.TimeBeforeFadeStart)
            {
                _textAlpha -= Time.deltaTime * _config.FadeSpeed;
                _startColor.a = _textAlpha;
            }
        }

        void IPoolable<PopupText>.SetPool(IObjectPool<PopupText> pool)
        {
            _pool = pool;
        }

        private void Reclaim() 
        {
            SceneContext.Instance.UpdateManager.Remove(this);

            _isCanMove = false;
            _pool.Release(this);
        }

        void IGameEntity.OnUpdate()
        {
            if (!_isCanMove) return;

            Move();            

            if (_textAlpha <= 0f)
            {
                Reclaim();
            }
        }
    }
}
