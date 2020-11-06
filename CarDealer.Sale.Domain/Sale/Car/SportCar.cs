using CarDealer.Domain.Common;
using CarDealer.Domain.SharedKernel;

namespace CarDealer.Domain.Sale.Car
{
    public class SportCar : AvailibleCar
    {
        private SportCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state, bool isReserved = false)
            : base(name, engine, transmission, currentMileage, basePrice, state, isReserved)
        {
            Type = CarType.Sport;
        }
        public static Result<SportCar> CreateCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state)
        {
            var carFactory = GetCarFactory<SportCar>(name, engine, transmission, currentMileage, basePrice, state);

            return carFactory(() => new SportCar(name, engine, transmission, currentMileage, basePrice, state));
        }

        public override double TaxBase
        {
            get
            {
                //you can make here some special computations dedicated to derived type:
                return 0.25;
            }
        }
    }
}
