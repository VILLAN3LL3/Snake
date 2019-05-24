﻿namespace Snake
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var interactors = new Interactors();
            var ui = new Ui();

            ui.PrepareConsole();

            ui.StartGame += () =>
            {
                interactors.StartGame((snake, points, feed) =>
                {
                    if (ui.ExcludeCollision(snake))
                    {
                        ui.UpdateSnake(snake);
                        ui.UpdatePoints(points);
                        ui.UpdateFeed(feed);
                    }
                    else
                    {
                        ui.AbortGame();
                    }
                });
            };


            ui.ChangeDirection += direction =>
                {
                    interactors.ChangeDirection(direction);
                };

            ui.Run();
        }
    }
}
