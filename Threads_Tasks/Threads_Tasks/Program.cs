using Threads_Tasks.Tools;
using Threads_Tasks.Model;

namespace Threads_Tasks
{
    public class Program
    {
        public static bool keepEating = true;
        public static void Main(string[] args)
        {
            const int numGuests = 5;
           
            Chopstick[] chopsticksTable = new Chopstick[numGuests];

            for (int i = 0; i < numGuests; i++) { chopsticksTable[i] = new Chopstick(); }

            List<Guest> guests = new List<Guest>();
            List<Thread> fils = new List<Thread>();

            for (int i = 0; i < numGuests; i++)
            {
                guests.Add(new Guest(i+1, chopsticksTable[i], chopsticksTable[(i + 1) % numGuests]));
            }
            foreach (var guest in guests)
            {
                Thread fil = new Thread(() => guest.Dinner());
                fils.Add(fil);
            }
            fils.ForEach(fil => fil.Start());
            fils.ForEach(fil => fil.Join());

            Tools.SaveStats.SaveStatsToCsv(guests);
        }
    }
}