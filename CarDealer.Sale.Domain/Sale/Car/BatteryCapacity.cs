using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class BatteryCapacity : ValueObject<BatteryCapacity>
    {
        public static readonly BatteryCapacity NonElectricOrHybridBatteryCapacity = new BatteryCapacity(0);

        private readonly string _displacementUnit = "kWh";
        private BatteryCapacity()
        {
        }
        public BatteryCapacity(decimal capacityInKwh) : this()
        {
            if (capacityInKwh < 0)
                throw new ArgumentException($"{capacityInKwh} must be equal or grater than zero.");
            CapacityInKwh = capacityInKwh;
        }

        public decimal CapacityInKwh { get; private set; }

        public override string ToString()
        {
            return $"{CapacityInKwh} {_displacementUnit}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CapacityInKwh;
        }
    }
}