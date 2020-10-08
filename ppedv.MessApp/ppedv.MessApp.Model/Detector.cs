namespace ppedv.MessApp.Model
{
    public class Detector : Entity
    {
        public string Name { get; set; }
        public virtual Messkomponente Komponente { get; set; }
    }

}
