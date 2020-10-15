using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class Engine : ValueObject<Engine>
    {
        private Engine()
        {
        }

        public Engine(EngineType type, EuroStandard euroStandard, EngineDisplacement engineCapacity, BatteryCapacity batteryCapacity) : this()
        {
            Type = type;
            EuroStandard = euroStandard ?? throw new ArgumentNullException(nameof(euroStandard));
            EngineCapacity = engineCapacity ?? throw new ArgumentNullException(nameof(engineCapacity));
            BatteryCapacity = batteryCapacity ?? throw new ArgumentNullException(nameof(batteryCapacity));
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