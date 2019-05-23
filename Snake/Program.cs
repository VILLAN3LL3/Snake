using System;

namespace Snake
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Clear();
            var interactors = new Interactors();
            var ui = new Ui();

            ui.StartGame += () =>
            {
                interactors.StartGame((snake, points, feed) =>
                {
                    ui.UpdateSnake(snake);
                    ui.UpdatePoints(points);
                    ui.UpdateFeed(feed);

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
