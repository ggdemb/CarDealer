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
        private BatteryCapacity(decimal capacityInKwh) : this()
        {
            CapacityInKwh = capacityInKwh;
        }
        public static Result<BatteryCapacity> Create(decimal? capacityInKwh)
        {
            if (!capacityInKwh.HasValue || capacityInKwh.Value == 0)
            {
                return Result.Ok(NonElectricOrHybridBatteryCapacity);
            }
            else
            {
                return Result.Ok(new BatteryCapacity(capacityInKwh.Value));
            }
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