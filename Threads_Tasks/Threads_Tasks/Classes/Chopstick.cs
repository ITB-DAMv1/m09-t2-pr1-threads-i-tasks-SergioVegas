using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads_Tasks.Model
{
    public class Chopstick
    {
        public int Id { get; set; }
        private readonly object _lock = new object();
        private Queue<Guest> waitingList = new Queue<Guest>();

        public bool TryAcquire(int timeout, Guest guest) 
        {
            lock (_lock)
            {
                if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(timeout)))
                {
                    return true; //Aconsegueix els dos palets
                }
                else
                {
                    waitingList.Enqueue(guest); // Va a la llista d'espera.
                    return false; 
                }
            }
        }

        public void Release()
        {
            lock (_lock)
            {
                Monitor.Exit(_lock);
                ProcessWaitingList();
            }
        }
        private void ProcessWaitingList()
        {
            lock (_lock)
            {
                List<Guest> stillWaiting = new List<Guest>();

                while (waitingList.Count > 0)
                {
                    Guest nextGuest = waitingList.Dequeue();

                    if (nextGuest.TryAcquireChopsticks()) // Intentem asignar els dos palets
                    {
                        return; 
                    }
                    else
                    {
                        stillWaiting.Add(nextGuest); // Si no ho aconsegueix, torna a la llista.
                    }
                }

                foreach (var guest in stillWaiting)
                {
                    waitingList.Enqueue(guest);
                }
            }
        }
    }
}
