using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class CarMileage : ValueObject<CarMileage>
    {
        private readonly string _mileageUnitLabel = "km";
        private static readonly int _minimalMileage = 0;

        private CarMileage()
        {
        }

        private CarMileage(int mileageInKm)
        {
            MileageInKm = mileageInKm;
        }

        public static Result<CarMileage> Create(int mileageInKm)
        {
            return (mileageInKm).ToResult()
                .Ensure(mileageInKm => mileageInKm > _minimalMileage, $"{nameof(mileageInKm)} must be grater than {_minimalMileage}.")
                .Map(mileageInKm => new CarMileage(mileageInKm));
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