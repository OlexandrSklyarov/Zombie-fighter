using System.Collections.Generic;
using AS.Gameplay.Map;
using SA.Gameplay.Data;
using UnityEngine;

namespace SA.Gameplay.Map
{
    public class MapController : IChankMap, IMovedPath
    {
        IEnumerable<MapChank> IChankMap.Chanks => _chanks;
        public Vector3 StartPoint => _startChank.StartPoint;
        public Vector3 EndPoint => _finishChank.FinishPoint;

        private Transform _world;
        private MapConfig _config;
        private List<MapChank> _chanks = new();
        private StartChank _startChank;
        private FinishChank _finishChank;

        public MapController(Transform world, MapConfig config)
        {
            _world = world;
            _config = config;
        }

        public void Generate()
        {      
            _startChank = GameObject.Instantiate(_config.StartChankPrefab, _world.position, Quaternion.identity, _world);
            
            var lastPosition = _world.position + Vector3.forward * _config.ChankOffset;

            for (int i = 0; i < _config.RoadLength; i++)
            {
                var chank = GameObject.Instantiate(_config.MapChankPrefab, lastPosition, Quaternion.identity, _world);
                lastPosition = _world.position + Vector3.forward * (i+1) * _config.ChankOffset;

                _chanks.Add(chank);
            }

            _finishChank = GameObject.Instantiate(_config.FinishChankPrefab, lastPosition, Quaternion.identity, _world);
        }
    }
}