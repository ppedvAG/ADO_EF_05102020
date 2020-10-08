using EfCodeFirst.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EfCodeFirst.Data
{
    public class EfContext : DbContext
    {
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Abteilung> Abteilungen { get; set; }
        public DbSet<Person> Person { get; set; }

        public EfContext(string conString) : base(conString)
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists)
            //Database.SetInitializer(new DropCreateDatabaseAlways<EfContext>());
            //   Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfContext, Migrations.Configuration>());
        }

        public EfContext() : this("name=myConString")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region konventionen
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(500));
            modelBuilder.Properties<string>().Where(x => x.Name == "Job").Configure(c => c.HasMaxLength(100).IsRequired());
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));
            #endregion

            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Kunde>().ToTable("Customers");
            modelBuilder.Entity<Mitarbeiter>().ToTable("Employees");

            modelBuilder.Entity<Mitarbeiter>().Property(x => x.Job).HasColumnName("Beruf").IsOptional();

            //modelBuilder.Entity<Abteilung>().Property(x => x.Id).HasColumnName("DerID");

            modelBuilder.Entity<Kunde>().HasRequired(x => x.Ansprechpartner).WithMany(x => x.IstAnsprechpartner);
            modelBuilder.Entity<Kunde>().HasRequired(x => x.Verkäufer).WithMany(x => x.IstVerkäufer);
        }
    }
}