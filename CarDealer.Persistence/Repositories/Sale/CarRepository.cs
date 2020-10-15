using CarDealer.Application.Sale;
using CarDealer.Domain.Sale.Car;

namespace CarDealer.Persistence.Repositories.Sale
{
    public class CarRepository : ICarRepository
    {
        public AvailibleCar GetAvailibleCar(long availibleCarId)
        {
            throw new System.NotImplementedException();
        }

        public AvailibleCarShortInfoDto GetAvailibleCarShortInfo(long availibleCarId)
        {
            throw new System.NotImplementedException();
        }
    }
}
