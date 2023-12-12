
namespace SA.Gameplay.GameEntity
{
    public interface IGameEntity
    {
        int InstanceID {get;}
        void OnUpdate();
    }
}