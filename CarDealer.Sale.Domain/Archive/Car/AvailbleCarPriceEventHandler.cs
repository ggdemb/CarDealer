using CarDealer.Domain.Common;
using CarDealer.Domain.Sale.Car;

namespace CarDealer.Domain.Archive.Car
{
    public class AvailbleCarPriceEventHandler : IHandler<AvailbleCarPriceChanged>
    {
        public void Handle(AvailbleCarPriceChanged domainEvent)
        {
            //do something in Archive bouded context, when somebody change car price;
        }
    }
}
