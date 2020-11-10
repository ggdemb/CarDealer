using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class EngineDisplacement : ValueObject<EngineDisplacement>
    {
        private static readonly EngineDisplacement ElectricEngineEngineDisplacement = new EngineDisplacement(0);

        private readonly string _displacementUnit = "cm3";
        private EngineDisplacement()
        {
        }
        private EngineDisplacement(decimal displacementInCm3) : this()
        {
            DisplacementInCm3 = displacementInCm3;
        }

        public static Result<EngineDisplacement> Create(decimal? engineDisplacementInCm3)
        {
            if (!engineDisplacementInCm3.HasValue || engineDisplacementInCm3.Value == 0)
            {
                return Result.Ok(ElectricEngineEngineDisplacement);
            }
            else
            {
                return Result.Ok(new EngineDisplacement(engineDisplacementInCm3.Value));
            }
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