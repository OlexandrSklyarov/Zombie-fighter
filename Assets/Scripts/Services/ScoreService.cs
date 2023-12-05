using System;

namespace SA.Services
{
    public class ScoreService
    {
        public int CurrentPoints
        {
            get => _currentPoints;
            private set
            {
                _currentPoints = value;
                ChangeScoreEvent?.Invoke(_currentPoints);
            }
        }

        private int _currentPoints;

        public event Action<int> ChangeScoreEvent;

        public ScoreService(int startPoints)
        {
            CurrentPoints = startPoints;
        }

        public void AddPoints(int points) => CurrentPoints += points;
    }
}
