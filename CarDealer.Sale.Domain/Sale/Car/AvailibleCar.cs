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
            var validationResults = "";

            if (IsReserved)
                validationResults += $"Car is reserved, price cannot be changed\n";

            if (validationResults == "")
                return Result.Ok();
            else
                return Result.Fail(validationResults);
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
            {
                return Result.Fail<AvailibleCar>($"Validation fail, cannot create {nameof(AvailibleCar)}");
            }
        }

        public static Result CanCreateCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice)
        {
            // you can place your bussines validations here:
            var validationResults = "";
            if (transmission == TransmissionType.Manual && engine.HasElectricEngine())
                validationResults += $"Car can't have {engine.Type} engine and {transmission}\n";


            if (validationResults == "")
                return Result.Ok();
            else
                return Result.Fail(validationResults);
        }

        protected static Func<Func<T>, Result<T>> GetCarFactory<T>(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, CarState state) where T : AvailibleCar
        {
            return (constructor) =>
            {
                var validationResult = CanCreateCar(name, engine, transmission, currentMileage, basePrice);
                if (validationResult.IsSuccess)
                {
                    var newCar = constructor();
                    //AddDomainEvent(new AvailbleCarCreated(Id));
                    return Result.Ok<T>(newCar);
                }
                else
                {
                    return Result.Fail<T>($"Validation fail, cannot create {typeof(T)}");
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
