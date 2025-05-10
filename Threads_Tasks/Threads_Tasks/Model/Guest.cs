using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public double MaxTimeHungry { get; set; }
        public Chopstick Right { get; set; }
        public Chopstick Left { get; set; }

        public Guest(int id, Chopstick right, Chopstick left)
        {
            Id = id;
            Right = right;
            Left = left;
            LastBite = DateTime.Now;
        }

        public void Dinner(bool keepEating )
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (keepEating)
            {

                Meditate();
                if (Hunger() || stopwatch.ElapsedMilliseconds >= 10000)
                {
                    lock (ConsoleLock)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Comensal {Id} ha estat massa temps sense menjar! Finalitzant la simulació.");
                        Console.ResetColor();
                    }
                    keepEating = false; 
                    return;
                }
                TakeChospticks();
                Eat();
                ReturnChopsticks();
            }
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
            lock (Right)
            {
                ShowState("Deixant el palet dret", ConsoleColor.Cyan);
            }

            lock (Left)
            {
                ShowState("Deixant el palet esquerre", ConsoleColor.Cyan);
            }
        }
        public void Eat()
        {
            ShowState("Menjant", ConsoleColor.Green);
            double timeHungry = (DateTime.Now - LastBite).TotalSeconds;//Calcul de quant temps porta sense menjar
            MaxTimeHungry = Math.Max(MaxTimeHungry, timeHungry); //Veiem si el nou temps registrat es més gran que l'ultim més gran.
            Thread.Sleep(new Random().Next(EatMinTime, EatMaxTime));
            CounterEat++;
            LastBite = DateTime.Now;
        }
        private void ShowState(string estat, ConsoleColor color)
        {
            lock (ConsoleLock)
            {
                Console.BackgroundColor = color;
                Console.ForegroundColor = (ConsoleColor)((Id % 15) + 1);
                Console.WriteLine($"Comensal {Id}: {estat}");
                Console.ResetColor();
            }
        }
        public bool Hunger()
        {
            // Console.WriteLine($"Comensal {Id} - {(DateTime.Now - LastBite).TotalMilliseconds}  Máximo permitido: {MaxTimeWithOutEat}s");
            return (DateTime.Now - LastBite).TotalMilliseconds > MaxTimeWithOutEat;  
        }
    }
}
