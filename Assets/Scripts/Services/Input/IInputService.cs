
using System;

namespace AS.Services.Input
{
    public interface IInputService
    {
        event Action OnTapEvent;
        event Action<float> OnHorizontalMoveEvent;

        void Enable();
        void Disable();
    }
}