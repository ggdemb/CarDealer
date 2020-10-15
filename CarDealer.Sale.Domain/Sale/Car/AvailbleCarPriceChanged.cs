using CarDealer.Domain.Common;

namespace CarDealer.Domain.Sale.Car
{
    public class AvailbleCarPriceChanged : IDomainEvent
    {
        public long AvailbleCarId { get; private set; }
        public decimal NewPriceInPln { get; private set; }

        public AvailbleCarPriceChanged(long availbleCarId, decimal newPriceInPln)
        {
            AvailbleCarId = availbleCarId;
            NewPriceInPln = newPriceInPln;
        }
    }
}