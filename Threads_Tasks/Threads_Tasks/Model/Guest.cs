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

        

    }
}
