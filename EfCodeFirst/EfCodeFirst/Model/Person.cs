using System;

namespace EfCodeFirst.Model
{
    public abstract class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Geschlecht Geschlecht { get; set; }

        public DateTime GebDatum { get; set; }
    }

    public enum Geschlecht
    {
        Divers,
        Weiblich,
        Männlich
    }
}