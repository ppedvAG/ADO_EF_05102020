using System;
using System.Collections.Generic;

namespace ppedv.MessApp.Model
{
    public class Messlauf : Entity
    {
        public DateTime Start { get; set; }
        public string GestartetVon { get; set; }
        public string GemessenesGerät { get; set; }
        public virtual ICollection<Messung> Messungen { get; set; } = new HashSet<Messung>();
    }

}
