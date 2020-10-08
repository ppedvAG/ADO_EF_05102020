using System.Collections.Generic;

namespace ppedv.MessApp.Model
{
    public class Messkomponente : Entity
    {
        public virtual Detector Detector { get; set; }
        public virtual Emitter Emitter { get; set; }
        public virtual ICollection<Messung> Messungen { get; set; } = new HashSet<Messung>();
    }

}
