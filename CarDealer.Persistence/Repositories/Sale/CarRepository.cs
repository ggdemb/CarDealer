using CarDealer.Application.Sale;
using CarDealer.Domain.Sale.Car;

namespace CarDealer.Persistence.Repositories.Sale
{
    public class CarRepository : ICarRepository
    {
        public AvailibleCar GetAvailibleCar(long availibleCarId)
        {
            //what to return? Result<AvailibleCar> or AvailibleCar and null?
            throw new System.NotImplementedException();
        }

        public AvailibleCarShortInfoDto GetAvailibleCarShortInfo(long availibleCarId)
        {
            throw new System.NotImplementedException();
        }
    }
}
