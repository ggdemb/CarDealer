using CarDealer.Domain.Sale.Car;
using CarDealer.Domain.SharedKernel;
using Xunit;

namespace CarDealer.Domain.Sale.Car.Tests
{
    public class AvailibleCarTests
    {
        [Fact()]
        public void CanCreateCar_CannotCreateElectricWithManualTransmission()
        {
            var transmission = TransmissionType.Manual;

            CarName name = new CarName("Tesla", "Model S");
            EuroStandard euroStandard = new EuroStandard(6);
            EngineDisplacement electricEngineEngineDisplacement = EngineDisplacement.ElectricEngineEngineDisplacement;
            BatteryCapacity batteryCapacity = new BatteryCapacity(50);
            Engine engine = new Engine(EngineType.FullyElectric, euroStandard, electricEngineEngineDisplacement, batteryCapacity);
            CarMileage currentMileage = new CarMileage(120_000);
            Pln basePrice = new Pln(150000);

            var result = AvailibleCar.CanCreateCar(name, engine, transmission, currentMileage, basePrice);

            Assert.Equal(result.Error, $"Car can't have {engine.Type} engine and {transmission}\n");
        }

        [Fact()]
        public void CanUpdatePrice_CannotChangePriceWhenCarIsReservedTest()
        {
            var transmission = TransmissionType.Automatic;

            CarName name = new CarName("Tesla", "Model S");
            EuroStandard euroStandard = new EuroStandard(6);
            EngineDisplacement electricEngineEngineDisplacement = EngineDisplacement.ElectricEngineEngineDisplacement;
            BatteryCapacity batteryCapacity = new BatteryCapacity(50);
            Engine engine = new Engine(EngineType.FullyElectric, euroStandard, electricEngineEngineDisplacement, batteryCapacity);
            CarMileage currentMileage = new CarMileage(120_000);
            Pln basePrice = new Pln(150000);
            var creationResult = AvailibleCar.CreateCar(name, engine, transmission, currentMileage, basePrice);
            var car = creationResult.Value;
            car.ToggleReservationState();

            var result = car.CanUpdatePrice();

            Assert.Equal(result.Error, $"Car is reserved, price cannot be changed\n");
        }
    }
}