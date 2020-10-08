using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.MessApp.Model;

namespace ppedv.MessApp.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_can_create_database()
        {
            var context = new EfContext();
            if (context.Database.Exists())
                context.Database.Delete();

            context.Database.Create();

            Assert.IsTrue(context.Database.Exists());
        }

        [TestMethod]
        public void EfContext_can_add_Messlauf()
        {
            var ml = new Messlauf()
            {
                Modified = DateTime.Now,
                Created = DateTime.Now,
                ModifiedBy = "Fred",
                GestartetVon = $"Fritz_{Guid.NewGuid()}",
                Start = DateTime.Now
            };

            using (var con = new EfContext())
            {
                con.Messläufe.Add(ml);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Messläufe.Find(ml.Id);
                Assert.AreEqual(ml.GestartetVon, loaded.GestartetVon);
            }
        }

        [TestMethod]
        public void EfContext_can_save_Messlauf_StartDatetime_full_range()
        {
            var mlNow = new Messlauf() { Start = DateTime.Now };
            var mlMax = new Messlauf() { Start = DateTime.MaxValue };
            var mlMin = new Messlauf() { Start = DateTime.MinValue };

            using (var con = new EfContext())
            {
                con.Messläufe.Add(mlNow);
                con.Messläufe.Add(mlMax);
                con.Messläufe.Add(mlMin);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loadedNow = con.Messläufe.Find(mlNow.Id);
                var loadedMax = con.Messläufe.Find(mlMax.Id);
                var loadedMin = con.Messläufe.Find(mlMin.Id);
                Assert.AreEqual(mlNow.Start, loadedNow.Start);
                Assert.AreEqual(mlMax.Start, loadedMax.Start);
                Assert.AreEqual(mlMin.Start, loadedMin.Start);
            }
        }

        [TestMethod]
        public void EfContext_is_CreatedDateTime_set_by_EF()
        {
            var d = new Detektor();

            using (var con = new EfContext())
            {
                con.Detektoren.Add(d);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Detektoren.Find(d.Id);

                //todo +/- 20ms
                Assert.AreEqual(DateTime.Now, loaded.Created);
            }
        }

        [TestMethod]
        public void EfContext_is_ModifiedDateTime_set_by_EF()
        {
            var d = new Detektor();
            using (var con = new EfContext())
            {
                con.Detektoren.Add(d);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Detektoren.Find(d.Id);
                loaded.Name = "Some changes";
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Detektoren.Find(d.Id);
                //todo +/- 20ms
                Assert.AreEqual(DateTime.Now, loaded.Modified);
            }
        }
    }
}
