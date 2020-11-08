using CarDealer.Domain.Common;

namespace CarDealer.Common.ExternalContracts
{
    public interface IBaseRepository<T> where T : AggregateRoot
    {
        public void Add(T objectToAdd);
    }
}
