﻿namespace EfCodeFirst.Model
{
    public class Kunde : Person
    {
        public string KdNummer { get; set; }
        public Mitarbeiter Mitarbeiter { get; set; }
    }

}
