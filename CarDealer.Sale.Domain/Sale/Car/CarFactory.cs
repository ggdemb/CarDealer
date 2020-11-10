using CarDealer.Domain.Common;

namespace CarDealer.Domain.Sale.Car
{
    public class CarFactory : ICarFactory
    {
        public Result<AvailibleCar> CreateCar(string brandName, string modelName, EngineType engineType, int euroStandart, decimal? engineDisplacementInCm3, decimal? batteryCapacityInKwh, TransmissionType transmissionType, int mileageInKm, decimal priceInPln, CarType carType, CarStateEnum state)
        {
            switch (carType)
            {
                case CarType.Sport:
                    return SportCar.CreateCar(brandName, modelName, engineType, euroStandart, engineDisplacementInCm3, batteryCapacityInKwh, transmissionType, mileageInKm, priceInPln, carType, state);
                case CarType.Regular:
                    return RegularCar.CreateCar(brandName, modelName, engineType, euroStandart, engineDisplacementInCm3, batteryCapacityInKwh, transmissionType, mileageInKm, priceInPln, carType, state);
                default:
                    return Result.Fail<AvailibleCar>($"Invalid value in {nameof(carType)} parameter");
            }
        }
    }
}
