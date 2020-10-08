using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ppedv.MessApp.Logic
{
    public class Core
    {
        public IRepository Repository { get; private set; }

        public Core(IRepository repo)
        {
            Repository = repo;
        }

        public int CountMessungOfDay(DateTime day)
        {
            return Repository.GetAll<Messung>().Count(x => x.MessZeit.Date == day.Date);
        }
    }
}
