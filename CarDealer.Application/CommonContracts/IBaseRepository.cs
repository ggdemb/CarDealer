using CarDealer.Domain.Common;

namespace CarDealer.Common.CommonContracts
{
    public interface IBaseRepository<T> where T : AggregateRoot
    {
        public void Add(T objectToAdd);
    }
}
