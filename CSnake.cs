namespace CSnake
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Linq;
    using System.Text;

    static class CSnake
    {
        internal static int currentDirection = 0;
        internal static int leftOffSet = Console.WindowWidth / 3;
        internal static int topOffSet = Console.WindowHeight / 3;
        internal static double sleepTime = 100;
        public struct Position
        {
            internal int x;
            internal int y;
            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Snake";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Queue<Position> snakeElements = new Queue<Position>();
            Random randomGenerator = new Random();

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
                Console.SetCursorPosition(item.x, item.y);
                Console.Write("*");
            }

            Position snakeHead = snakeElements.Last();
            Console.SetCursorPosition(snakeHead.x, snakeHead.y);

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

                Console.SetCursorPosition(food.x, food.y);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("+");

                Position prevSnakeHead = snakeElements.Last();
                Position newSnakeHead = new Position(
                    prevSnakeHead.x + moveDirections[currentDirection].x,
                    prevSnakeHead.y + moveDirections[currentDirection].y
                );

                if (newSnakeHead.x >= Console.WindowWidth ||
                    newSnakeHead.x < 0 ||
                    newSnakeHead.y >= Console.WindowHeight ||
                    newSnakeHead.y < 0 ||
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

                Console.SetCursorPosition(prevSnakeHead.x, prevSnakeHead.y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("*");

                snakeElements.Enqueue(newSnakeHead);

                Console.SetCursorPosition(newSnakeHead.x, newSnakeHead.y);
                Console.Write("@");

                if (newSnakeHead.x == food.x && newSnakeHead.y == food.y)
                {
                    food = new Position(
                        randomGenerator.Next(1, Console.WindowWidth - 1),
                        randomGenerator.Next(1, Console.WindowHeight - 1));

                    Console.SetCursorPosition(food.x, food.y);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("+");
                }
                else
                {
                    Position p = snakeElements.Dequeue();

                    Console.SetCursorPosition(p.x, p.y);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }

                Thread.Sleep((int)sleepTime);
                sleepTime -= 0.09;
            }
        }
    }
}
