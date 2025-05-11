using Asteroide.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroide.Classes
{
    public class Player
    {
        public int Position { get; private set; }

        public Player(int initialPosition)
        {
            Position = initialPosition;
        }

        public void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.A && Position > 2) Position -= 2;
            if (key == ConsoleKey.D && Position < Console.WindowWidth - 10) Position += 2;
        }

        public void ResetPosition(int initialPosition)
        {
            Position = initialPosition;
        }

        public bool CollidesWith(Asteroid asteroid)
        {
            int shipLeft = Position;
            int shipRight = Position + Character.cat[0].Length;
            int shipTop = Console.WindowHeight - Character.cat.Length - 2;
            int shipBottom = Console.WindowHeight - 2;

            return asteroid.X >= shipLeft && asteroid.X <= shipRight &&
                   asteroid.Y >= shipTop && asteroid.Y <= shipBottom;
        }
    }
}
