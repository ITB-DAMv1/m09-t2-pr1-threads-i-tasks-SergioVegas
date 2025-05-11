using Asteroide.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroide.Classes
{
    class Renderer
    {
        public static void DrawPlayer(int playerPos)
        {
            int posX = playerPos;
            int posY = Console.WindowHeight - Character.cat.Length - 2;

            for (int i = 0; i < Character.cat.Length; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Character.cat[i]);
                Console.ResetColor();
            }
        }

        public static void DrawAsteroids(List<Asteroid> asteroids)
        {
            lock (asteroids)
            {
                foreach (var asteroid in asteroids)
                {
                    int posX = asteroid.X;
                    int posY = asteroid.Y;

                    posY = Math.Max(0, Math.Min(posY, Console.WindowHeight - Character.drop.Length - 1));

                    for (int i = 0; i < Character.drop.Length; i++)
                    {
                        if (posY + i >= Console.WindowHeight) break;
                        Console.SetCursorPosition(posX, posY + i);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(Character.drop[i]);
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
