﻿using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Exceptions;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;

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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));

            modelBuilder.Entity<Emitter>().HasOptional(x => x.Komponente).WithOptionalDependent(x => x.Emitter);
            modelBuilder.Entity<Detektor>().HasOptional(x => x.Komponente).WithOptionalDependent(x => x.Detektor);

            modelBuilder.Entity<Messlauf>().Property(x => x.Modified).IsConcurrencyToken(true);

        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                if (item.Entity is Entity en)
                {
                    en.Created = DateTime.Now;
                    en.Modified = DateTime.Now;
                }
            }

            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
            {
                if (item.Entity is Entity en)
                    en.Modified = DateTime.Now;
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var dbparaEx = new DbParalellExcpetion();

                dbparaEx.DatabaseWins = () =>
                {
                    foreach (var item in ex.Entries)
                    {
                        item.CurrentValues.SetValues(item.GetDatabaseValues());
                    }

                };

                dbparaEx.UserWins = () =>
                {
                    foreach (var item in ex.Entries)
                    {
                        item.OriginalValues.SetValues(item.GetDatabaseValues());
                    }
                    SaveChanges();
                };

                throw dbparaEx;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
