using System.Collections.Generic;
using System.Linq;
using SA.Gameplay.Data;
using SA.Gameplay.Map;
using SA.Services;
using SA.Services.ObjectPool;
using UnityEngine;

namespace SA.Gameplay.Enemies
{
    public class EnemyController
    {
        private readonly IChankMap _map;
        private readonly PoolManager _poolService;
        private readonly LevelConfig _levelConfig;
        private readonly List<EnemyUnit> _units = new();
        private bool _isRunning;

        public EnemyController(IChankMap map, LevelConfig levelConfig)
        {
            _map = map;
            _poolService = SceneContext.Instance.PoolGoService;
            _levelConfig = levelConfig;
        }

        public void GenerateEnemies()
        {
            var chankCount = _map.Chanks.Count();

            for (int i = 0; i < chankCount; i++)
            {
                var chank = _map.Chanks.ElementAt(i);
                int unitsCount = GetUnitCountPerChank(chankCount, i);

                for (int x = 0; x < unitsCount; x++)
                {
                    var rndPos = GetRandomPosition(chank.Center.position, chank.Bounce);
                    var rndRot = GetRandomRotation();

                    var unit = _poolService.GetUnit(_levelConfig.EnemyPrefab);
                    unit.transform.SetPositionAndRotation(rndPos, rndRot);

                    unit.Init();
                    unit.DestroyEvent += OnUitDestroy;

                    _units.Add(unit);
                }
            }

            _isRunning = true;
        }

        private void OnUitDestroy(EnemyUnit unit)
        {
            unit.DestroyEvent -= OnUitDestroy;
            _units.Remove(unit);

            SceneContext.Instance.PlayerStatsService.AddPoints(unit.KillCost);
            SceneContext.Instance.VfxService.CreatePopupText
            (
                unit.transform.position + Vector3.up * 3f + Vector3.right * UnityEngine.Random.Range(-3f, 3f),
                $"+{unit.KillCost}",
                Color.yellow
            );
        }

        private int GetUnitCountPerChank(int count, int i)
        {
            var progress = (i + 1) / (float)count;
            var multiplier = _levelConfig.EnemyDensityPerChank.Evaluate(progress);
            var unitsCount = Mathf.RoundToInt(_levelConfig.MaxUnitCountPerChank * multiplier);
            
            return Mathf.Max(1, unitsCount);
        }

        private Quaternion GetRandomRotation()
        {
            return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up);
        }

        private Vector3 GetRandomPosition(Vector3 center, Vector2 areaBounce)
        {
            areaBounce *= 0.5f;

            var pos = new Vector3
            (
                UnityEngine.Random.Range(-areaBounce.x, areaBounce.x),
                0f,
                UnityEngine.Random.Range(-areaBounce.y, areaBounce.y)
            );

            return center + pos;
        }

        public void OnUpdate()
        {
            if (!_isRunning) return;

            foreach(var unit in _units.ToList())
            {
                unit.OnUpdate();
            }
        }

        public void StopEnemies()
        {
            _isRunning = false;

            foreach(var unit in _units.ToList())
            {
                unit.OnStop();
                unit.DestroyEvent -= OnUitDestroy;
            }
        }
    }
}
