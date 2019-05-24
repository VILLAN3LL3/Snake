using System;

namespace Snake
{
    internal class Program
    {
        private static Interactors _interactors;

        private static void Main(string[] args)
        {

            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            _interactors = new Interactors();
            var ui = new Ui();

            ui.PrepareConsole();

            ui.StartGame += () =>
            {
                _interactors.StartGame((snake, points, feed, level) =>
                {
                    if (ui.ExcludeCollision(snake))
                    {
                        ui.UpdateSnake(snake);
                        ui.UpdatePoints(points);
                        ui.UpdateFeed(feed);
                        ui.UpdateLevel(level);
                    }
                    else
                    {
                        ui.AbortGame();
                    }
                });
            };

            ui.ShowHighscore += () =>
            {
                _interactors.UpdateHighscore(highscore =>
                {
                    ui.UpdateHighscore(highscore);
                });
            };

            ui.ChangeDirection += direction =>
                {
                    _interactors.ChangeDirection(direction);
                };

            ui.Run();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            _interactors.SerializeHighscsore();
        }
    }
}
