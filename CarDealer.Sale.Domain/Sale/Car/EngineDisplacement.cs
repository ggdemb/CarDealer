using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class EngineDisplacement : ValueObject<EngineDisplacement>
    {
        public static readonly EngineDisplacement ElectricEngineEngineDisplacement = new EngineDisplacement(0);

        private readonly string _displacementUnit = "cm3";
        private EngineDisplacement()
        {
        }
        public EngineDisplacement(decimal displacementInCm3) : this()
        {
            if (displacementInCm3 < 0)
                throw new ArgumentException($"{displacementInCm3} must be equal or grater than zero.");
            DisplacementInCm3 = displacementInCm3;
        }

        public decimal DisplacementInCm3 { get; private set; }

        public override string ToString()
        {
            return $"{DisplacementInCm3} {_displacementUnit}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DisplacementInCm3;
        }
    }
}