using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;

namespace ppedv.MessApp.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void Core_GetAverageMessResultOfDay_2_Messläufe_with_2_Messung_results_66()
        {
            var core = new Core(new TestRepository());

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().Be(66);
        }

        [TestMethod]
        public void Core_GetAverageMessResultOfDay_2_Messläufe_with_2_Messung_results_66_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Messlauf>()).Returns(() => 
            {
                var ml1 = new Messlauf() { Start = DateTime.Now };
                var ml2 = new Messlauf() { Start = DateTime.Now };

                ml1.Messungen.Add(new Messung() { Messwert = 33 });
                ml1.Messungen.Add(new Messung() { Messwert = 99 });

                ml2.Messungen.Add(new Messung() { Messwert = 33 });
                ml2.Messungen.Add(new Messung() { Messwert = 99 });

                return new[] { ml1, ml2 };
            });
            var core = new Core(mock.Object);

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().Be(66);
        }

        [TestMethod]
        public void Core_GetAverageMessResultOfDay_2_Messläufe_with_0_Messung_results_null()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Messlauf>()).Returns(() =>
            {
                var ml1 = new Messlauf() { Start = DateTime.Now };
                var ml2 = new Messlauf() { Start = DateTime.Now };

                return new[] { ml1, ml2 };
            });
            var core = new Core(mock.Object);

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Core_GetAverageMessResultOfDay_0_Messläufe_results_null()
        {
            var mock = new Mock<IRepository>();
            var core = new Core(mock.Object);

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().BeNull();
        }
    }

    class TestRepository : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            if (typeof(T) == typeof(Messlauf))
            {
                var ml1 = new Messlauf() { Start = DateTime.Now };
                var ml2 = new Messlauf() { Start = DateTime.Now };

                ml1.Messungen.Add(new Messung() { Messwert = 33 });
                ml1.Messungen.Add(new Messung() { Messwert = 99 });

                ml2.Messungen.Add(new Messung() { Messwert = 33 });
                ml2.Messungen.Add(new Messung() { Messwert = 99 });

                return new[] { ml1, ml2 }.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public int SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}
