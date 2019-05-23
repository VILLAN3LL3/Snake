﻿using System;

namespace Snake
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.Clear();
            Console.Write("Neues Spiel = N Spiel verlassen = X");
            var interactors = new Interactors();
            var ui = new Ui();

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
