namespace SA.Gameplay.Weapons
{
    public interface IShootable
    {
        void OnUpdate();
        void SetRotation(float deltaRotation);
    }
}