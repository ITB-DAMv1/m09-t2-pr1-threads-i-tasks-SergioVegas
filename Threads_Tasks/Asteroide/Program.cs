using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Asteroide.Characters;
namespace Asteroide
{
    class Program
    {
        static int width = 120;  
        static int height = 60;
        static int playerPos = width / 2;
        static bool gameOver = false;
        static List<(int x, int y)> asteroids = new List<(int, int)>();
        static int score = 0;

        static async Task Main()
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.CursorVisible = false;

            Task inputTask = Task.Run(() => HandleInput());
            Task asteroidTask = Task.Run(() => GenerateAsteroids());

            while (!gameOver)
            {
                Console.Clear();
                DrawPlayer();
                MoveAsteroids();
                DrawAsteroids();
                CheckCollision();
                Console.SetCursorPosition(0, height - 1);
                Console.Write($"Score: {score}");

                await Task.Delay(50);
            }

            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Final Score: {score}");
        }

        static void HandleInput()
        {
            while (!gameOver)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.A && playerPos > 2) playerPos -= 2;  // Movemos más rápido
                if (key == ConsoleKey.D && playerPos < width - 3) playerPos += 2;
                if (key == ConsoleKey.Q) gameOver = true;
            }
        }

        static void GenerateAsteroids()
        {
            Random rand = new Random();
            while (!gameOver)
            {
                lock (asteroids)
                {
                    asteroids.Add((rand.Next(2, width - 2), 0)); // Asteroides más dispersos
                }
                Task.Delay(400).Wait();
            }
        }

        static void MoveAsteroids()
        {
            lock (asteroids)
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    asteroids[i] = (asteroids[i].x, asteroids[i].y + 1);
                    if (asteroids[i].y >= height) asteroids.RemoveAt(i);
                }
            }
        }

        static void DrawPlayer()
        {
            int posX = playerPos;
            int posY = Console.WindowHeight - Character.cat.Length - 2;

            for (int i = 0; i < Character.cat.Length; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.WriteLine(Character.cat[i]);
            }
        }

        static void DrawAsteroids()
        {
            lock (asteroids)
            {
                foreach (var asteroid in asteroids)
                {
                    int posX = asteroid.x;
                    int posY = asteroid.y;

                    
                    posY = Math.Max(0, Math.Min(posY, Console.WindowHeight - Character.drop.Length - 1));

                    for (int i = 0; i < Character.drop.Length; i++)
                    {
                        if (posY + i >= Console.WindowHeight) break; 
                        Console.SetCursorPosition(posX, posY + i);
                        Console.WriteLine(Character.drop[i]);
                    }
                }
            }
        }

        static void CheckCollision()
        {
            lock (asteroids)
            {
                foreach (var asteroid in asteroids)
                {
                    if (asteroid.y == height - 3 && Math.Abs(asteroid.x - playerPos) < 2)
                    {
                        gameOver = true;
                        return;
                    }
                }
            }
            score++;
        }
    }
}
