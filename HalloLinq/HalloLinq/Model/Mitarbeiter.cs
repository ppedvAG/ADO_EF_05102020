using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloLinq
{
    public class Mitarbeiter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime GebDatum { get; set; }
        public List<Abteilung> Abteilungen { get; set; } = new List<Abteilung>();
    }
}
