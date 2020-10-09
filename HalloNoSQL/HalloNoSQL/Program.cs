using LiteDB;
using System;

namespace HalloNoSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new LiteDatabase(@"db.db"))
            {
                var col = db.GetCollection<Auto>("Autos");

                col.Insert(new Auto() { Hersteller = "Bercedes", KW = 2000 });

                foreach (var a in col.Query().ToList())
                {
                    Console.WriteLine($"{a.Id} {a.Hersteller}");
                }


            }


        }
    }

    public class Auto
    {
        public int Id { get; set; }
        public string Hersteller { get; set; }

        public int KW { get; set; }

    }
}
