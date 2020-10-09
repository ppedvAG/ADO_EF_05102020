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
            return Repository.Query<Messung>().Count(x => x.MessZeit.Year == day.Year && x.MessZeit.Month == day.Month && x.MessZeit.Day == day.Day);
        }

        public decimal? GetAverageMessResultOfDay(DateTime day)
        {
            var query = Repository.Query<Messlauf>().Where(x => x.Start.Year == day.Year && x.Start.Month == day.Month && x.Start.Day == day.Day)
                                                     .SelectMany(x => x.Messungen);
            if (query.Count() == 0)
                return null;

            return query.Average(x => x.Messwert);
        }
    }
}
