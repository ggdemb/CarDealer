using CarDealer.Domain.Common;

namespace CarDealer.Domain.Sale.Car
{
    public class SportCar : AvailibleCar
    {
        private SportCar()
        {

        }
        private SportCar(string test)
        {
            var test2 = test;
        }
        internal static Result<AvailibleCar> CreateCar(string brandName,
            string modelName,
            EngineType engineType,
            int euroStandart,
            decimal? engineDisplacementInCm3,
            decimal? batteryCapacityInKwh,
            TransmissionType transmissionType,
            int mileageInKm,
            decimal priceInPln,
            CarType carType,
            CarStateEnum state)
        {
            var carFactory = GetCarFactory(brandName, modelName, engineType, euroStandart, engineDisplacementInCm3, batteryCapacityInKwh, transmissionType, mileageInKm, priceInPln, carType, state);
            return carFactory(() => new SportCar(null));
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
