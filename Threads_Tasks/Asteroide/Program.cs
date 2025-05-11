using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Asteroide.Characters;
using Asteroide.Classes;
namespace Asteroide
{
    class Program
    {
        static async Task Main()
        {
            Game game = new Game();
            await game.Start();
        }
    }
}