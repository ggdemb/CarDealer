using CarDealer.Domain.Common;
using CarDealer.Domain.SharedKernel;

namespace CarDealer.Domain.Sale.Car
{
    public class RegularCar : AvailibleCar
    {
        private RegularCar()
        {

        }
        private RegularCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state, bool isReserved = false)
            : base(name, engine, transmission, currentMileage, basePrice, state, isReserved)
        {
            Type = CarType.Regular;
        }
        public static Result<RegularCar> CreateCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state)
        {
            var carFactory = GetCarFactory<RegularCar>(name, engine, transmission, currentMileage, basePrice, state);

            return carFactory(() => new RegularCar(name, engine, transmission, currentMileage, basePrice, state));
        }



        public override double TaxBase
        {
            get
            {
                //you can make here some special computations dedicated to derived type:
                return 0.15;
            }
        }
    }
}
