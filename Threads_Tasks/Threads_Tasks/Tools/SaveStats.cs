using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threads_Tasks.Model;

namespace Threads_Tasks.Tools
{
    public class SaveStats
    {
        public static void SaveStatsToCsv(List<Guest> guests)
        {
            string folderPath = @"..\..\..\Files";  
            string filePath = Path.Combine(folderPath, "guest_stats.csv");
            StringBuilder csvContent = new StringBuilder();

            // Encabezados
            csvContent.AppendLine("GuestId, MaxTimeHungry, MealsCount");

            // Registrar datos de cada comensal
            foreach (var guest in guests)
            {
                csvContent.AppendLine($"{guest.Id},{guest.MaxTimeHungry},{guest.CounterEat}");
            }

            // Guardar archivo
            File.WriteAllText(filePath, csvContent.ToString());
            Console.WriteLine("¡Estadísticas guardadas en guest_stats.csv!");
        }
    }
}
