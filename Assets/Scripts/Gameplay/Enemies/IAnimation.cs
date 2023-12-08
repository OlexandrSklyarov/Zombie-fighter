
namespace SA.Gameplay.Enemies
{
    public interface IAnimation
    {
        void Idle();
        void Move();
        float Attack();
        float Damage();
    }
}