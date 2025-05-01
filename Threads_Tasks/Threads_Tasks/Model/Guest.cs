using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads_Tasks.Model
{
    public class Guest
    {
        private static readonly object ConsoleLock = new object(); //L'utilitcem per ordenar els missatges que donem a l'usuari i no es barregin missatges d'un comensal amb un altre.
        private static int MedidateMinTime = 500;
        private static int MedidateMaxTime = 2000;
        private static int EatMinTime = 500;
        private static int EatMaxTime = 1000;
        private static int MaxTimeWithOutEat = 15000;
        public int Id { get; set; } 
        public DateTime LastBite { get; set; }
        public int  CounterEat {  get; set; } = 0; 
        public Chopstick Right { get; set; }
        public Chopstick Left { get; set; }

        public Guest(int id, Chopstick right, Chopstick left)
        {
            Id = id;
            Right = right;
            Left = left;
        }

        public void Meditate()
        {
            ShowState("Pensat...", ConsoleColor.Blue);
            Thread.Sleep(new Random().Next(MedidateMinTime, MedidateMaxTime));
        }
        public void TakeChospticks()
        {
            lock (Right)
            {
                ShowState("Agafant el palet dret", ConsoleColor.Yellow);
                lock (Left) { ShowState("Agafant el palet esquerre", ConsoleColor.Yellow); }
            }
        }
        public void ReturnChopsticks()
        {
            ShowState("Deixant els palets a la taula", ConsoleColor.Magenta);
        }
        public void Eat()
        {
            ShowState("Menjant", ConsoleColor.Red);
            Thread.Sleep(new Random().Next(EatMinTime, EatMaxTime));
            CounterEat++;
            LastBite = DateTime.Now;
        }
        private void ShowState(string estat, ConsoleColor color)
        {
            lock (ConsoleLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"Comensal{Id}: {estat}");
                Console.ResetColor();
            }
        }
        public bool Hunger()
        {
            return (DateTime.Now - LastBite).TotalMilliseconds > MaxTimeWithOutEat;
        }
    }
}
