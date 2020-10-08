using ppedv.MessApp.Model;
using System.Data.Entity;


namespace ppedv.MessApp.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Messlauf> Messläufe { get; set; }
        public DbSet<Messung> Messungen { get; set; }
        public DbSet<Messkomponente> Messkomponenten { get; set; }
        public DbSet<Detektor> Detektoren { get; set; }
        public DbSet<Emitter> Emitter { get; set; }

        public EfContext(string conString) : base(conString)
        { }

        public EfContext() : this("Server=(localdb)\\mssqllocaldb;Database=MessApp_dev;Trusted_Connection=true")
        { }
    }
}
