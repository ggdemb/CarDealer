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



        public AvailibleCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state, bool isReserved = false)
        {
            Name = name;
            Engine = engine;
            Transmission = transmission;
            CurrentMileage = currentMileage;
            BasePrice = basePrice;
            State = state;
            IsReserved = isReserved;
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
                 .Ensure(x => x.IsNotReserved, $"Car is reserved, price cannot be changed.")
                 .SkipPayload();
        }

        public Result UpdatePrice(decimal newPriceInPln)
        {
            var validationResult = CanUpdatePrice();
            if (validationResult.IsSuccess)
            {
                BasePrice = new Pln(newPriceInPln);
                AddDomainEvent(new AvailbleCarPriceChanged(Id, newPriceInPln));
                return Result.Ok();
            }
            else
                return Result.Fail(validationResult.Errors);
        }

        public static Result CanCreateCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice)
        {
            // you can place your bussines validations here:
            return (name, engine, transmission, currentMileage, basePrice).ToResult()
                 .Ensure(x => x.engine.HasElectricEngine() && x.transmission == TransmissionType.Automatic, $"Car can't have electric engine and manual transmission.")
                 .SkipPayload();
        }

        protected static Func<Func<T>, Result<T>> GetCarFactory<T>(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state) where T : AvailibleCar
        {
            return (constructor) =>
            {
                var validationResult = CanCreateCar(name, engine, transmission, currentMileage, basePrice);
                if (validationResult.IsSuccess)
                {
                    var newSpecificCar = constructor();
                    //AddDomainEvent(new AvailbleCarCreated(Id));
                    return Result.Ok<T>(newSpecificCar);
                }
                else
                    return Result.Fail<T>(validationResult.Errors);

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
        public void UpdateCurrentMileage(int newMileageInKm)
        {
            CurrentMileage = new CarMileage(newMileageInKm);
        }

        private readonly List<CarHistoryItem> _carHistory;
        public IReadOnlyList<CarHistoryItem> CarHistory { get => _carHistory.ToList(); }
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
