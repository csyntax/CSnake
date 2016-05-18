using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;

namespace CSnake
{
    public static class CSnake
    {
        private static int currentDirection = 0;
        private static int leftOffSet = Console.WindowWidth / 3;
        private static int topOffSet = Console.WindowHeight / 3;
        private static double sleepTime = 100;
        private static Random randomGenerator = new Random();

        public static void Main(string[] args)
        {
            Console.Title = "Snake";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Queue<Position> snakeElements = new Queue<Position>();
            
            Position[] moveDirections = new Position[] {
                new Position(1, 0), 
                new Position(0, 1), 
                new Position(-1, 0), 
                new Position(0, -1), 
            };

            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight;

            Position food = new Position(
                randomGenerator.Next(1, Console.WindowWidth - 1),
                randomGenerator.Next(1, Console.WindowHeight - 1)
            );

            for (int i = 0; i <= 6; i++)
            {
                snakeElements.Enqueue(new Position(i, 0));
            }

            foreach (var item in snakeElements)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write("*");
            }

            Position snakeHead = snakeElements.Last();
            Console.SetCursorPosition(snakeHead.X, snakeHead.Y);

            Console.Write("@");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey();

                    if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (currentDirection != 2) currentDirection = 0;
                    }
                    if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        if (currentDirection != 3) currentDirection = 1;
                    }
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        if (currentDirection != 0) currentDirection = 2;
                    }
                    if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        if (currentDirection != 1) currentDirection = 3;
                    }
                }

                Console.SetCursorPosition(food.X, food.Y);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("+");

                Position prevSnakeHead = snakeElements.Last();
                Position newSnakeHead = new Position(
                    prevSnakeHead.X + moveDirections[currentDirection].X,
                    prevSnakeHead.Y + moveDirections[currentDirection].Y
                );

                if (newSnakeHead.X >= Console.WindowWidth ||
                    newSnakeHead.X < 0 ||
                    newSnakeHead.Y >= Console.WindowHeight ||
                    newSnakeHead.Y < 0 ||
                    snakeElements.Contains(newSnakeHead))
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.SetCursorPosition(leftOffSet, topOffSet);
                    Console.WriteLine("Game over!!! Your points: {0}", snakeElements.Count);
                    Console.Beep();

                    Thread.Sleep(1000);

                    return;
                }

                Console.SetCursorPosition(prevSnakeHead.X, prevSnakeHead.Y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("*");

                snakeElements.Enqueue(newSnakeHead);

                Console.SetCursorPosition(newSnakeHead.X, newSnakeHead.Y);
                Console.Write("@");

                if (newSnakeHead.X == food.X && newSnakeHead.Y == food.Y)
                {
                    food = new Position(
                        randomGenerator.Next(1, Console.WindowWidth - 1),
                        randomGenerator.Next(1, Console.WindowHeight - 1));

                    Console.SetCursorPosition(food.X, food.Y);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("+");
                }
                else
                {
                    Position p = snakeElements.Dequeue();

                    Console.SetCursorPosition(p.X, p.Y);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }

                Thread.Sleep((int)sleepTime);
                sleepTime -= 0.09;
            }
        }
    }
}