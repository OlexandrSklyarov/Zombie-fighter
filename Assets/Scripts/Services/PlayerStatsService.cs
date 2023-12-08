using System;
using UnityEngine;

namespace SA.Services
{
    public class PlayerStatsService
    {
        #region 
        public struct PlayerStats
        {
            public int Level;
            public int Vehicle;
            public int Weapon;
            public int Points; 
        }
        #endregion

        public int CurrentLevel {get; private set;}
        public int CurrentVehicle {get; private set;}
        public int CurrentWeapon {get; private set;}
        public int CurrentPoints 
        {
            get => _currentPoints;
            private set
            {
                _currentPoints = value;
                ChangePointsEvent?.Invoke(_currentPoints);
            }
        }

        private int _currentPoints;

        private const string DATA_KEY = "PlayerStats";

        public event Action<int> ChangePointsEvent;

        public PlayerStatsService()
        {
            LoadData();            
        }        

        public void AddPoints(int points)
        {
            CurrentPoints += points;
        }

        public void Save()
        {
            var data = JsonUtility.ToJson(new PlayerStats
            {
                Level = CurrentLevel,
                Vehicle = CurrentVehicle,
                Weapon = CurrentWeapon,
                Points = CurrentPoints
            });

            PlayerPrefs.SetString(DATA_KEY, data);
        }

        private void LoadData()
        {
            var str = PlayerPrefs.GetString(DATA_KEY, string.Empty);           

            var data = (str == string.Empty) ?
                default :
                JsonUtility.FromJson<PlayerStats>(str);

            CurrentLevel = data.Level;
            CurrentVehicle = data.Vehicle;
            CurrentWeapon = data.Weapon;
            CurrentPoints = data.Points;
        }
    }
}
