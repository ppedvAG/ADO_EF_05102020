namespace EfCodeFirst.Model
{
    public class Kunde : Person
    {
        public string KdNummer { get; set; }

        public int Größe { get; set; }
        public int Zellennummer { get; set; }
        public virtual Mitarbeiter Ansprechpartner { get; set; }
        public virtual Mitarbeiter Verkäufer { get; set; }
    }

}
