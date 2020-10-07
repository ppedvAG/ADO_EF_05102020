using System.Collections.Generic;

namespace EfCodeFirst.Model
{
    public class Mitarbeiter : Person
    {
        public string Job { get; set; }
        public ICollection<Kunde> Kunden { get; set; } = new HashSet<Kunde>();
        public ICollection<Abteilung> Abteilungen { get; set; } = new HashSet<Abteilung>();

    }

}
