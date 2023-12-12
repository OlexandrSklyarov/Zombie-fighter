
namespace SA.Gameplay.GameEntity
{
    public interface IUpdateManager
    {
        void Add(IGameEntity entity);
        void Remove(IGameEntity entity);
        void OnUpdate();
        void Clear();
    }
}