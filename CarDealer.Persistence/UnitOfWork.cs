using CarDealer.Application.CommonContracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace CarDealer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarDealerContext _carDealerContext;
        private readonly IDbContextTransaction _scopeTransaction;

        public UnitOfWork(ICarDealerContext carDealerContext)
        {
            _carDealerContext = (CarDealerContext)carDealerContext ?? throw new ArgumentNullException(nameof(carDealerContext));
            _scopeTransaction = _carDealerContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _scopeTransaction.Commit();
            _carDealerContext.HandleDomainEvents(); // what if handler need db context?
        }

        public void Rollback()
        {
            _scopeTransaction.Rollback();
        }
    }
}
