using System.Collections.Generic;
using AS.Gameplay.Map;
using SA.Gameplay.Data;
using UnityEngine;

namespace SA.Gameplay.Map
{
    public class MapController : IChankMap, IMovedPath
    {
        IEnumerable<MapChank> IChankMap.Chanks => _chanks;
        Vector3 IMovedPath.StartPoint => _startChank.StartPoint;
        Vector3 IMovedPath.EndPoint => _finishChank.FinishPoint;

        private Vector3 _startPosition;
        private MapConfig _config;
        private List<MapChank> _chanks;
        private StartChank _startChank;
        private FinishChank _finishChank;

        public MapController(Vector3 startPosition, MapConfig config)
        {
            _startPosition = startPosition;
            _config = config;
        }

        public void Generate()
        {      
            _startChank = GameObject.Instantiate(_config.StartChankPrefab, _startPosition, Quaternion.identity);
            
            var lastPosition = _startPosition + Vector3.forward * _config.ChankOffset;

            for (int i = 0; i < _config.RoadLength; i++)
            {
                var chank = GameObject.Instantiate(_config.MapChankPrefab, lastPosition, Quaternion.identity);
                lastPosition = _startPosition + Vector3.forward * (i+1) * _config.ChankOffset;
            }

            _finishChank = GameObject.Instantiate(_config.FinishChankPrefab, lastPosition, Quaternion.identity);
        }
    }
}