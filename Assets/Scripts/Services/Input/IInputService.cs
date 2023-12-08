
using System;

namespace AS.Services.Input
{
    public interface IInputService
    {
        bool IsTapScreen { get; }

        event Action OnTapEvent;
        event Action<float> OnHorizontalMoveEvent;

        void Enable();
        void Disable();
    }
}