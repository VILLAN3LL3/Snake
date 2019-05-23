using System;
using System.Collections.Generic;

namespace Snake
{
    public class Interactors
    {
        private Direction _direction;
        private List<Coordinate> _snake;
        private readonly TimerProvider _timer;

        public Interactors()
        {
            _timer = new TimerProvider();
        }

        public void StartGame(Action<List<Coordinate>> onNewSnake)
        {
            _snake = Snakes.NewSnake();
            _direction = Snakes.InitialDirection();
            _timer.StartTimer(() =>
            {
                _snake = Snakes.MoveSnake(_snake, _direction);
                onNewSnake(_snake);
            });
        }

        public void ChangeDirection(Direction direction)
        {
            _direction = direction;
        }
    }
}
