using System;

namespace CarDealer.Application.Sale
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        public void ChangeAvailibleCarPrice(long availbleCarId, decimal newPriceInPln)
        {
            var carToChange = _carRepository.GetAvailibleCar(availbleCarId);

            if (carToChange.CanUpdatePrice().IsSuccess)
            {
                carToChange.UpdatePrice(newPriceInPln);
            }
        }
    }
}
