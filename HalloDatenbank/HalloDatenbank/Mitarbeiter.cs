using System;

namespace HalloDatenbank
{
    class Mitarbeiter
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Stadt { get; set; }
        public DateTime GebDatum { get; set; }
        public DateTime EinstellDatum { get; set; }
    }
}
