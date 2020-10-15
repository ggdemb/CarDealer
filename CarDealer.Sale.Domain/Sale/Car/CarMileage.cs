using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class CarMileage : ValueObject<CarMileage>
    {
        private readonly string _mileageUnitLabel = "km";

        private CarMileage()
        {
        }

        public CarMileage(int mileageInKm) : this()
        {
            if (mileageInKm <= 0)
                throw new ArgumentException($"{mileageInKm} must be grater than zero.");
            MileageInKm = mileageInKm;
        }

        public int MileageInKm { get; private set; }

        public override string ToString()
        {
            return $"{MileageInKm} {_mileageUnitLabel}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MileageInKm;
        }
    }
}