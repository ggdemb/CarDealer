using CarDealer.Domain.Common;

namespace CarDealer.Domain.Sale.Car
{
    public interface ICarFactory
    {
        Result<AvailibleCar> CreateCar(string brandName,
            string modelName,
            EngineType engineType,
            int euroStandart,
            decimal? engineDisplacementInCm3,
            decimal? batteryCapacityInKwh,
            TransmissionType transmissionType,
            int mileageInKm,
            decimal priceInPln,
            CarType carType,
            CarStateEnum state);
    }
}
