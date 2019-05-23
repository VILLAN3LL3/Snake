using System;
using System.Collections.Generic;

namespace Snake
{
    public class Interactors
    {
        private Direction _direction;
        private List<Coordinate> _snake;
        private readonly TimerProvider _timer;
        private Coordinate _feed;
        private int _points;

        public Interactors()
        {
            _timer = new TimerProvider();
        }

        public void StartGame(Action<List<Coordinate>, int, Coordinate> onNewSnake)
        {
            _snake = Snakes.NewSnake();
            _direction = Snakes.InitialDirection();
            _feed = Snakes.NewFeed();

            _timer.StartTimer(() =>
            {
                _snake = Snakes.MoveSnake(_snake, _direction);
                if (Snakes.SnakeIsEating(_snake, _feed))
                {
                    _feed = Snakes.NewFeed();
                }
                else
                {
                    _snake = Snakes.ShortenSnake(_snake);
                }

                _points = Snakes.CalcPoints(_snake, _points);
                onNewSnake(_snake, _points, _feed);
            });
        }

        public void ChangeDirection(Direction direction)
        {
            _direction = direction;
        }
    }
}
