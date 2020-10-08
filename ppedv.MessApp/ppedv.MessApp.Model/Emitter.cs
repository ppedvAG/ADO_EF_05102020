namespace ppedv.MessApp.Model
{
    public class Emitter : Entity
    {
        public string Name { get; set; }
        public virtual Messkomponente Komponente { get; set; }
    }

}
