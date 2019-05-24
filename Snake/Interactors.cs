using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly List<HighscoreElement> _highscore;

        public Interactors()
        {
            _timer = new TimerProvider();
            var highscoreFile = File.ReadAllText("Highscore.json");
            _highscore = JsonConvert.DeserializeObject<List<HighscoreElement>>(highscoreFile);
        }

        public void StartGame(Action<List<Coordinate>, int, Coordinate, int> onNewSnake)
        {
            Console.Clear();
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

                onNewSnake(_snake, _points, _feed, _level);
            });
        }

        public void ChangeDirection(Direction direction)
        {
            _direction = direction;
        }
    }
}
