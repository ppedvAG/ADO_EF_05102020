using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ppedv.MessApp.Model.Contracts
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        void Add<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;


        int SaveAll();
    }
}
