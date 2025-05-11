using Asteroide.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroide.Classes
{
    public class Asteroid
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move()
        {
            Y += 2;
        }

        public bool ShouldDisappear(int windowHeight)
        {
            return Y >= windowHeight - Character.drop.Length;
        }
    }
}
