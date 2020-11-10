using CarDealer.Domain.Common;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class Engine : ValueObject<Engine>
    {
        private Engine()
        {
        }

        private Engine(EngineType type, EuroStandard euroStandard, EngineDisplacement engineCapacity, BatteryCapacity batteryCapacity) : this()
        {
            Type = type;
            EuroStandard = euroStandard;
            EngineCapacity = engineCapacity;
            BatteryCapacity = batteryCapacity;
        }

        public static Result<Engine> Create(EngineType engineType, int euroStandart, decimal? engineDisplacementInCm3, decimal? batteryCapacityInKwh)
        {

            var euroStandartResult = EuroStandard.CreateEuroStandard(euroStandart);
            var engineDisplacementResult = EngineDisplacement.Create(engineDisplacementInCm3);
            var batteryCapacityResult = BatteryCapacity.Create(batteryCapacityInKwh);

            var result = Result.CombineErrors(euroStandartResult.Errors, engineDisplacementResult.Errors, batteryCapacityResult.Errors);

            if (result.IsFailure)
                return Result.Fail<Engine>(result.Errors);
            else
            {
                return Result.Ok(new Engine(engineType, euroStandartResult.Value, engineDisplacementResult.Value, batteryCapacityResult.Value));
            }
        }

        public bool HasElectricEngine()
        {
            return Type == EngineType.FullyElectric || Type == EngineType.Hybrid;
        }

        public EngineType Type { get; private set; }
        public EuroStandard EuroStandard { get; private set; }
        public EngineDisplacement EngineCapacity { get; private set; }
        public BatteryCapacity BatteryCapacity { get; private set; }

        public override string ToString()
        {
            return $"{EngineCapacity.ToString()} {BatteryCapacity.ToString()} {Type} {EuroStandard.ToString()}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return EuroStandard.Value;
            yield return EngineCapacity.DisplacementInCm3;
            yield return BatteryCapacity.CapacityInKwh;

        }
    }
    public enum EngineType
    {
        Diesel,
        Petrol,
        Hybrid,
        FullyElectric
    }
}