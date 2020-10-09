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
            var core = new Core(new TestUnitOfWork());

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().Be(66);
        }

        [TestMethod]
        public void Core_GetAverageMessResultOfDay_2_Messläufe_with_2_Messung_results_66_moq()
        {
            var repoMock = new Mock<IRepository<Messlauf>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetRepo<Messlauf>()).Returns(repoMock.Object);
            repoMock.Setup(x => x.Query()).Returns(() =>
            {
                var ml1 = new Messlauf() { Start = DateTime.Now };
                var ml2 = new Messlauf() { Start = DateTime.Now };

                ml1.Messungen.Add(new Messung() { Messwert = 33 });
                ml1.Messungen.Add(new Messung() { Messwert = 99 });

                ml2.Messungen.Add(new Messung() { Messwert = 33 });
                ml2.Messungen.Add(new Messung() { Messwert = 99 });

                return new[] { ml1, ml2 }.AsQueryable();
            });
            var core = new Core(uowMock.Object);

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().Be(66);
        }

        [TestMethod]
        public void Core_GetAverageMessResultOfDay_2_Messläufe_with_0_Messung_results_null()
        {
            var repoMock = new Mock<IRepository<Messlauf>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetRepo<Messlauf>()).Returns(repoMock.Object);
            repoMock.Setup(x => x.Query()).Returns(() =>
            {
                var ml1 = new Messlauf() { Start = DateTime.Now };
                var ml2 = new Messlauf() { Start = DateTime.Now };

                return new[] { ml1, ml2 }.AsQueryable();
            });
            var core = new Core(uowMock.Object);

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Core_GetAverageMessResultOfDay_0_Messläufe_results_null()
        {
            var repoMock = new Mock<IRepository<Messlauf>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GetRepo<Messlauf>()).Returns(repoMock.Object);
            var core = new Core(uowMock.Object);

            var result = core.GetAverageMessResultOfDay(DateTime.Now);

            result.Should().BeNull();
        }
    }

    public class TestUnitOfWork : IUnitOfWork
    {
        public IRepository<T> GetRepo<T>() where T : Entity
        {
            if (typeof(T) == typeof(Messlauf))
                return new MesslaufTestRepositroy<Messlauf>().As<IRepository<T>>();

            throw new NotImplementedException();
        }

        public int SaveAll()
        {
            throw new NotImplementedException();
        }
    }

    public class MesslaufTestRepositroy<T> : IRepository<Messlauf> where T : Entity
    {
        public void Add(Messlauf entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Messlauf entity)
        {
            throw new NotImplementedException();
        }

        public Messlauf GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Messlauf> Query()
        {
            var ml1 = new Messlauf() { Start = DateTime.Now };
            var ml2 = new Messlauf() { Start = DateTime.Now };

            ml1.Messungen.Add(new Messung() { Messwert = 33 });
            ml1.Messungen.Add(new Messung() { Messwert = 99 });

            ml2.Messungen.Add(new Messung() { Messwert = 33 });
            ml2.Messungen.Add(new Messung() { Messwert = 99 });

            return new[] { ml1, ml2 }.AsQueryable();
        }

        public void Update(Messlauf entity)
        {
            throw new NotImplementedException();
        }
    }


}
