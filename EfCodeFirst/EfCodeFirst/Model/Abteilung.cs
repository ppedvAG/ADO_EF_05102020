using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCodeFirst.Model
{
    [Table("Department")]
    public class Abteilung
    {
        [Column("DieID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(40)]
        public string Bezeichnung { get; set; }
        
        public bool AlleDoof { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Mitarbeiter> Mitarbeiter { get; set; } = new HashSet<Mitarbeiter>();
    }

}
