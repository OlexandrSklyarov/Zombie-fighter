using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AS.Services.Input
{
    public class TouchInputService : IInputService
    {
        private DefaultInputActions _inputActions;

        public event Action OnTapEvent;
        public event Action<float> OnHorizontalMoveEvent;

        public TouchInputService()
        {
            _inputActions = new DefaultInputActions();
            _inputActions.Player.Fire.started += (_) => OnTapEvent?.Invoke();
            _inputActions.Player.Look.performed += (ctx) => OnHorizontalMoveEvent?.Invoke(ctx.ReadValue<Vector2>().x);

            Enable();
        }

        public void Enable()
        {
            _inputActions?.Enable();
        }

        public void Disable()
        {
            _inputActions?.Disable();
        }
    }
}