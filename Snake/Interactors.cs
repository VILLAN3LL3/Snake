using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Snake
{
    public class Interactors
    {
        private Direction _direction;
        private List<Coordinate> _snake;
        private readonly TimerProvider _timer;
        private Coordinate _feed;
        private int _points;
        private int _level = 1;
        private List<HighscoreElement> _highscore;
        private HighscoreElement _currentHighscoreElement;
        private string _userName;

        public Interactors()
        {
            _timer = new TimerProvider();
            DeserializeHighscore();

        }

        public void StartGame(Action<List<Coordinate>, int, Coordinate, int> onNewSnake)
        {
            Console.Clear();
            _userName = Environment.UserName;
            HighscoreElement element = _highscore.Find(h => h.UserName.Equals(_userName, StringComparison.InvariantCultureIgnoreCase));
            if (element != null)
            {
                _currentHighscoreElement = element;
            }
            else
            {
                _currentHighscoreElement = new HighscoreElement() { UserName = _userName, Points = _points };
                _highscore.Add(_currentHighscoreElement);
            }

            _snake = Snakes.NewSnake();
            _direction = Snakes.InitialDirection();
            _feed = Snakes.NewFeed();

            _timer.StartTimer(() =>
            {
                _snake = Snakes.MoveSnake(_snake, _direction);
                if (Snakes.SnakeIsEating(_snake, _feed))
                {
                    _feed = Snakes.NewFeed();
                    if (Snakes.SnakeReachedNextLevel(_snake))
                    {
                        _level++;
                        _timer.SpeedUpTimer();
                    }
                }
                else
                {
                    _snake = Snakes.ShortenSnake(_snake);
                }

                _points = Snakes.CalcPoints(_snake, _points);
                _currentHighscoreElement.Points = Math.Max(_currentHighscoreElement.Points, _points);

                onNewSnake(_snake, _points, _feed, _level);
            });
        }

        internal void UpdateHighscore(Action<IEnumerable<HighscoreElement>> onUpdateHighscore)
        {
            onUpdateHighscore(_highscore.OrderByDescending(h => h.Points));
        }

        private void DeserializeHighscore()
        {
            var highscoreFile = File.ReadAllText("Highscore.json");
            _highscore = JsonConvert.DeserializeObject<List<HighscoreElement>>(highscoreFile);

        }

        internal void SerializeHighscsore()
        {
            var highscoreText = JsonConvert.SerializeObject(_highscore.OrderByDescending(h => h.Points));
            File.WriteAllText("Highscore.json", highscoreText);
        }

        public void ChangeDirection(Direction direction)
        {
            _direction = direction;
        }
    }
}
