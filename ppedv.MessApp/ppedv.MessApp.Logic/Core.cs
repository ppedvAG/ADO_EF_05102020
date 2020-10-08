using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System;
using System.Linq;

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

        public decimal? GetAverageMessResultOfDay(DateTime day)
        {
            var query = Repository.GetAll<Messlauf>().Where(x => x.Start.Date == day.Date)
                                                     .SelectMany(x => x.Messungen);
            if (query.Count() == 0)
                return null;

            return query.Average(x => x.Messwert);
        }
    }
}
