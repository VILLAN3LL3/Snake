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
                interactors.StartGame(snake =>
                ui.UpdateSnake(snake));
            };

            ui.ChangeDirection += direction =>
            {
                interactors.ChangeDirection(direction);
            };

            ui.Run();
        }
    }
}
