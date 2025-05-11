using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NAudio.Wave;

namespace Asteroide.Classes
{
    class Game
    {
        private DateTime startTime;
        private int width = 120;
        private int height = 60;
        private Player player;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private bool gameOver = false;
        private int score = 0;
        private bool isPlaying = true;

        public Game()
        {
            player = new Player(width / 2);
        }

        public async Task Start()
        {
            startTime = DateTime.Now;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.CursorVisible = false;

            Task.Run(() => HandleInput());
            Task.Run(() => GenerateAsteroids());

            while (!gameOver)
            {
                await Task.Delay(20); // 50 Hz
                MoveAsteroids();
                CheckCollision();
                if ((DateTime.Now - startTime).TotalSeconds >= 60)
                {
                    gameOver = true;
                    ShowVictoryScreen();
                }

                await Task.Delay(50); //20 Hz
                Console.Clear();
                Renderer.DrawPlayer(player.Position);
                Renderer.DrawAsteroids(asteroids);
                Console.SetCursorPosition(0, height - 1);
                Console.Write($"Score: {score}");
            }
        }
        private void ShowVictoryScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Felicitats! Has sobreviscut durant 1 minut!!!");
            Console.WriteLine($"Puntuació final: {score}");
            Console.WriteLine("Prem qualsevol tecla per sortir.");
            Console.ReadKey(true);
        }

        private void HandleInput()
        {
            while (!gameOver)
            {
                var key = Console.ReadKey(true).Key;
                if (isPlaying)
                {
                    player.Move(key);
                    if (key == ConsoleKey.Q)
                    {
                        gameOver = true;
                        EndGame();
                    }
                }
                else
                {
                    isPlaying = true;
                    ResetGame();
                }
            }
        }

        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine($"Final Score: {score}");
            Task.Delay(500).Wait(); 
            gameOver = true;
        }

        private void GenerateAsteroids()
        {
            Random rand = new Random();
            while (!gameOver)
            {
                lock (asteroids)
                {
                    asteroids.Add(new Asteroid(rand.Next(2, width - 2), 0));
                }
                Console.Beep(1000, 100); //So per cada gota
                Task.Delay(400).Wait();
            }
        }

        private void MoveAsteroids()
        {
            lock (asteroids)
            {
                foreach (var asteroid in asteroids)
                {
                    asteroid.Move();
                }

                // Eliminem les gotes que cauen.
                asteroids.RemoveAll(a => a.ShouldDisappear(height));
            }
        }

        private void CheckCollision()
        {
            lock (asteroids)
            {
                foreach (var asteroid in asteroids)
                {
                    if (player.CollidesWith(asteroid))
                    {
                        MakeNoise();
                        isPlaying = false;
                        ShowGameOverScreen();
                        return;
                    }
                }
            }
            score++;
        }

        private void ResetGame()
        {
            player.ResetPosition(width / 2);
            asteroids.Clear();
            score = 0;
        }
        private void MakeNoise()
        {
            using (var audioFile = new AudioFileReader("../../../Sounds/meow.mp3"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                Thread.Sleep(1000);
            }
        }

        private void ShowGameOverScreen()
        {
            Console.Clear();
            Console.WriteLine("El gat s'ha mullat :(\nSi vols intentar de nou ajudar al gat, clica qualsevol tecla!");
            Console.WriteLine($"Final Score: {score}");
            Console.ReadKey(true);
            ResetGame();
        }
    }
}
