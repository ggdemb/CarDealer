using CarDealer.Domain.Common;

namespace CarDealer.Domain.Sale.Car
{
    public class RegularCar : AvailibleCar
    {
        private RegularCar()
        {

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
            //maybe better use Activator?
            var carFactory = GetCarFactory(brandName, modelName, engineType, euroStandart, engineDisplacementInCm3, batteryCapacityInKwh, transmissionType, mileageInKm, priceInPln, carType, state);
            return carFactory(() => new RegularCar());
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
