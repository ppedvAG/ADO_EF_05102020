using ppedv.MessApp.Logic;
using ppedv.MessApp.Model;
using System;

namespace ppedv.MessApp.UI.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var core = new Core(new Data.EF.EfRepository());

            Console.WriteLine($"Messungen heute: {core.CountMessungOfDay(DateTime.Now)}");

            foreach (var ml in core.Repository.GetAll<Messlauf>())
            {
                Console.WriteLine($"{ml.GestartetVon} {ml.Start} {ml.GemessenesGerät}");
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
