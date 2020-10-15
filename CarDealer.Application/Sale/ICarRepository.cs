using CarDealer.Domain.Sale.Car;

namespace CarDealer.Application.Sale
{
    public interface ICarRepository
    {
        AvailibleCar GetAvailibleCar(long availibleCarId);
        AvailibleCarShortInfoDto GetAvailibleCarShortInfo(long availibleCarId);
    }
}
