using System;
using System.Collections.Generic;

namespace EfCodeFirst.Model
{
    public class Mitarbeiter : Person
    {
        public string Job { get; set; }

        public bool IstGefeuert { get; set; }
        public DateTime GefeuertAn { get; set; }

        

        public virtual ICollection<Kunde> IstAnsprechpartner { get; set; } = new HashSet<Kunde>();
        public virtual ICollection<Kunde> IstVerkäufer { get; set; } = new HashSet<Kunde>();

        public virtual ICollection<Abteilung> Abteilungen { get; set; } = new HashSet<Abteilung>();

    }

}
