namespace ppedv.MessApp.Model
{
    public class Detektor : Entity
    {
        public string Name { get; set; }
        public virtual Messkomponente Komponente { get; set; }
    }

}
