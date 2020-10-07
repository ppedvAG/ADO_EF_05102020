using System;
using System.Collections.Generic;

namespace EfCodeFirst.Model
{
    public class Abteilung
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Bezeichnung { get; set; }
        public ICollection<Mitarbeiter> Mitarbeiter { get; set; } = new HashSet<Mitarbeiter>();
    }

}
