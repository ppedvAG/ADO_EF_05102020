using System;

namespace ppedv.MessApp.Model
{
    public class Messung : Entity
    {
        public DateTime MessZeit { get; set; }
        public virtual Messkomponente Komponente { get; set; }
        public string Messparameter { get; set; }
        public decimal Messwert { get; set; }
    }

}
