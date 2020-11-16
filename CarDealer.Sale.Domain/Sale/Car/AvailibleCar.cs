using CarDealer.Domain.Common;
using CarDealer.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Domain.Sale.Car
{
    public abstract class AvailibleCar : AggregateRoot
    {
        protected AvailibleCar()
        {
            _carHistory = new List<CarHistoryItem>();
        }

        public void ToggleReservationState()
        {
            IsReserved = !IsReserved;
        }
        public abstract double TaxBase
        {
            get;
        }
        public Result CanUpdatePrice()
        {
            return (this).ToResult()
                 .Ensure(new NotFixedCarPriceSpecification().IsSatisfiedBy(this), $"Car is reserved, price cannot be changed.")
                 .SkipPayload();
        }

        public Result UpdatePrice(decimal newPriceInPln)
        {
            var carValidationResult = CanUpdatePrice();
            if (carValidationResult.IsSuccess)
            {
                var priceResult = Pln.Create(newPriceInPln);
                if (priceResult.IsSuccess)
                {
                    BasePrice = priceResult.Value;
                    AddDomainEvent(new AvailbleCarPriceChanged(Id, newPriceInPln));
                    return Result.Ok();
                }
                else
                    return Result.Fail(priceResult.Errors);
            }
            else
                return Result.Fail(carValidationResult.Errors);
        }

        private static Result CanCreateCar(Result<CarName> carNameResult, Result<Engine> engineResult, TransmissionType transmission, Result<CarMileage> mileageResult, Result<Pln> priceResult)
        {

            var subElementsResult = Result.CombineErrors(carNameResult.Errors, engineResult.Errors, mileageResult.Errors, priceResult.Errors);
            if (subElementsResult.IsSuccess)
            {
                return (carName: carNameResult.Value, engine: engineResult.Value, transmission, mileage: mileageResult.Value, price: priceResult.Value).ToResult()
                     .Ensure(x => !new ElectricEngineSpecification().IsSatisfiedBy(x.engine) || x.transmission == TransmissionType.Automatic, $"Car can't have electric engine and manual transmission.")
                     .SkipPayload();
            }
            else
                return Result.Fail("Car must have valid all sub elements.");

        }


        protected static Func<Func<AvailibleCar>, Result<AvailibleCar>> GetCarFactory(
            string brandName,
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
            return (constructor) =>
            {
                var carNameResult = CarName.Create(brandName, modelName);
                var engineResult = Engine.Create(engineType, euroStandart, engineDisplacementInCm3, batteryCapacityInKwh);
                var mileageResult = CarMileage.Create(mileageInKm);
                var priceResult = Pln.Create(priceInPln);
                var carValidationResult = CanCreateCar(carNameResult, engineResult, transmissionType, mileageResult, priceResult);

                var combinedResult = Result.CombineErrors(carNameResult.Errors, engineResult.Errors, mileageResult.Errors, priceResult.Errors, carValidationResult.Errors);

                if (combinedResult.IsFailure)
                    return Result.Fail<AvailibleCar>(combinedResult.Errors);
                else
                {
                    var newSpecificCar = constructor();

                    newSpecificCar.State = state;
                    newSpecificCar.Type = carType;
                    newSpecificCar.Transmission = transmissionType;
                    newSpecificCar.Name = carNameResult.Value;
                    newSpecificCar.Engine = engineResult.Value;
                    newSpecificCar.Transmission = transmissionType;
                    newSpecificCar.CurrentMileage = mileageResult.Value;
                    newSpecificCar.BasePrice = priceResult.Value;
                    newSpecificCar.State = state;

                    ///AddDomainEvent(new AvailbleCarCreated(Id)); //Use HiLo, but dispatch event on repository or on overrided Id seter (EF will set Id);
                    return Result.Ok(newSpecificCar);
                }
            };
        }

        public void AddHistoryPosition(CarHistoryItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _carHistory.Add(item);
        }
        public Result UpdateCurrentMileage(int newMileageInKm)
        {
            var result = CarMileage.Create(newMileageInKm);
            if (result.IsSuccess)
            {
                CurrentMileage = result.Value;
                return Result.Ok();
            }
            else
                return result.SkipPayload();
        }

        private readonly List<CarHistoryItem> _carHistory;
        public IReadOnlyList<CarHistoryItem> CarHistory => _carHistory.ToList();
        public CarName Name { get; private set; }
        public CarState State { get; private set; }
        public Engine Engine { get; private set; }
        public TransmissionType Transmission { get; private set; }
        public CarType Type { get; protected set; }
        public CarMileage CurrentMileage { get; private set; }
        public Pln BasePrice { get; private set; }
        public bool IsReserved { get; private set; }
        public bool IsNotReserved => !IsReserved;
    }
    public enum TransmissionType : byte
    {
        Manual,
        Automatic
    }

    public enum CarType : byte
    {
        Sport,
        Regular
    }
}
