using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class EuroStandard : ValueObject<EuroStandard>
    {
        private readonly string _standardLabael = "EURO";
        private EuroStandard()
        {
        }

        public EuroStandard(int value) : this()
        {
            if (value <= 0)
                throw new ArgumentException($"{value} must be grater than zero.");
            Value = value;
        }


        public int Value { get; private set; }

        public override string ToString()
        {
            return $"{_standardLabael} {Value}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}