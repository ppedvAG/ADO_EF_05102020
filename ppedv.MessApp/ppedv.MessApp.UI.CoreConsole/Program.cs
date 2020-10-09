using Autofac;
using ppedv.MessApp.Logic;
using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace ppedv.MessApp.UI.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var ass = Assembly.LoadFrom("ppedv.MessApp.Data.EF.dll");
            var builder = new ContainerBuilder();
            //builder.RegisterType<EfRepository>().As<IRepository>();
            builder.RegisterAssemblyTypes(ass).Where(x => x.Name.EndsWith("UnitOfWork")).AsImplementedInterfaces();
            var container = builder.Build();

            var core = new Core(container.Resolve<IUnitOfWork>());

            Console.WriteLine($"Messungen heute: {core.CountMessungOfDay(DateTime.Now)}");
            Console.WriteLine($"Durchschnitt: {core.GetAverageMessResultOfDay(DateTime.Now)}");

            Console.WriteLine($"Anzahl Messung: {core.UnitOfWork.GetRepo<Messung>().Query().Count()}");

            foreach (var ml in core.UnitOfWork.GetRepo<Messlauf>().Query())
            {
                Console.WriteLine($"{ml.GestartetVon} {ml.Start} {ml.GemessenesGerät}");
            }

            Console.WriteLine("Messläufe geladen per Stored Procedure: ");
            foreach (Messlauf messlauf in core.UnitOfWork.MesslaufRepository.GetMesslaufeByStoredProc())
            {
                Console.WriteLine($"per Stored Procedure: {messlauf.Start}");
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
