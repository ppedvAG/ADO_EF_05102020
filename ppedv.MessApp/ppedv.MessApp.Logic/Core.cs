using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.MessApp.Logic
{
    public class Core
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        public Core(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public int CountMessungOfDay(DateTime day)
        {
            return UnitOfWork.GetRepo<Messung>().Query().Count(x => x.MessZeit.Year == day.Year && x.MessZeit.Month == day.Month && x.MessZeit.Day == day.Day);
        }

        public decimal? GetAverageMessResultOfDay(DateTime day)
        {
            var query = UnitOfWork.GetRepo<Messlauf>().Query().Where(x => x.Start.Year == day.Year && x.Start.Month == day.Month && x.Start.Day == day.Day)
                                                     .SelectMany(x => x.Messungen);
            if (query.Count() == 0)
                return null;

            return query.Average(x => x.Messwert);
        }
    }
}
