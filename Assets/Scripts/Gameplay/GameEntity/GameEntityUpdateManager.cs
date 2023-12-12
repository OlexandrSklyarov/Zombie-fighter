using System.Collections.Generic;
using System.Linq;

namespace SA.Gameplay.GameEntity
{
    public sealed class GameEntityUpdateManager : IUpdateManager
    {
        private Dictionary<int, IGameEntity> _entities = new();

        public void Add(IGameEntity entity)
        {
            if (_entities.ContainsKey(entity.InstanceID)) return;
            
            _entities.Add(entity.InstanceID, entity);
        }

        public void Remove(IGameEntity entity)
        {
            if (!_entities.ContainsKey(entity.InstanceID)) return;
            
            _entities.Remove(entity.InstanceID);
        }

        public void OnUpdate()
        {
            foreach(var ent in _entities.ToList())
            {
                ent.Value?.OnUpdate();
            }
        }

        public void Clear() => _entities.Clear();
    }
}