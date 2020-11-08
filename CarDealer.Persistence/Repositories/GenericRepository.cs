using CarDealer.Application.ExternalContracts;
using CarDealer.Common.ExternalContracts;
using CarDealer.Domain.Common;
using System;

namespace CarDealer.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : AggregateRoot
    {
        private readonly CarDealerContext _carDealerContext;

        public BaseRepository(ICarDealerContext carDealerContext)
        {
            _carDealerContext = (CarDealerContext)carDealerContext ?? throw new ArgumentNullException(nameof(carDealerContext));
        }

        public void Add(T objectToAdd)
        {
            _carDealerContext.Set<T>().Add(objectToAdd);
        }
    }
}
