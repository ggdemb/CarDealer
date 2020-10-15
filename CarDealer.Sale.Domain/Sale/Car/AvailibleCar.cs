using CarDealer.Domain.Common;
using CarDealer.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Domain.Sale.Car
{
    public class AvailibleCar : AggregateRoot
    {
        private AvailibleCar()
        {
            _carHistory = new List<CarHistoryItem>();
        }

        public AvailibleCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice, bool isReserved = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
            Transmission = transmission;
            CurrentMileage = currentMileage ?? throw new ArgumentNullException(nameof(currentMileage));
            BasePrice = basePrice ?? throw new ArgumentNullException(nameof(basePrice));
            IsReserved = isReserved;
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
            if (validationResult.Success)
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
            //do not mix application error with bussines errors (Result is only for bussines errors)
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (engine is null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (currentMileage is null)
            {
                throw new ArgumentNullException(nameof(currentMileage));
            }

            if (basePrice is null)
            {
                throw new ArgumentNullException(nameof(basePrice));
            }

            // you can place your bussines validations here:
            // todo: maybe Result shoud have collections of strings for error messages, or even better collection of objects with Code, and Message
            var validationResults = "";
            if (transmission == TransmissionType.Manual && engine.HasElectricEngine())
                validationResults += $"Car can't have {engine.Type} engine and {transmission}\n";


            if (validationResults == "")
                return Result.Ok();
            else
                return Result.Fail(validationResults);
        }
        public static Result<AvailibleCar> CreateCar(CarName name, Engine engine, TransmissionType transmission, CarMileage currentMileage, Pln basePrice)
        {
            var validationResult = CanCreateCar(name, engine, transmission, currentMileage, basePrice);
            if (validationResult.Success)
            {
                var newCar = new AvailibleCar(name, engine, transmission, currentMileage, basePrice);
                //AddDomainEvent(new AvailbleCarCreated(Id));
                return Result.Ok(newCar);
            }
            else
            {
                return Result.Fail<AvailibleCar>($"Validation fail, cannot create {nameof(AvailibleCar)}");
            }
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
        public Engine Engine { get; private set; }
        public TransmissionType Transmission { get; private set; }
        public CarMileage CurrentMileage { get; private set; }
        public Pln BasePrice { get; private set; }
        public bool IsReserved { get; private set; }
    }
    public enum TransmissionType
    {
        Manual,
        Automatic
    }
}
