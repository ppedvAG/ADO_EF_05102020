using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.MessApp.Data.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        EfContext con = new EfContext();

        public IMesslaufRepository MesslaufRepository => new EfMesslaufRepository(con);

        public IRepository<T> GetRepo<T>() where T : Entity
        {
            return new EfRepository<T>(con);
        }

        public int SaveAll()
        {
            return con.SaveChanges();
        }
    }

    public class EfMesslaufRepository : EfRepository<Messlauf>, IMesslaufRepository
    {
        public EfMesslaufRepository(EfContext context) : base(context)
        { }

        public IEnumerable<Messlauf> GetMesslaufeByStoredProc()
        {
            var query = con.Database.SqlQuery<Messlauf>("SELECT * FROM Messlauf");
            return query.ToList();
        }
    }

    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected EfContext con;
        public EfRepository(EfContext context)
        {
            con = context;
        }

        public void Add(T entity)
        {
            con.Set<T>().Add(entity);

        }

        public void Delete(T entity)
        {
            con.Set<T>().Remove(entity);

        }

        public T GetById(int id)
        {
            return con.Set<T>().Find(id);
        }

        public IQueryable<T> Query()
        {
            return con.Set<T>();
        }

        public void Update(T entity)
        {
            var loaded = GetById(entity.Id);
            if (loaded != null)
                con.Entry(loaded).CurrentValues.SetValues(entity);
        }
    }
}
