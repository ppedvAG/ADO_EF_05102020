using System;
using System.Collections;

namespace ppedv.MessApp.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
    }
}
