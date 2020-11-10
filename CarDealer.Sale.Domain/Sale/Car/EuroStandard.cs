using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class EuroStandard : ValueObject<EuroStandard>
    {
        private readonly string _standardLabael = "EURO";
        private static readonly int _minEuro = 0;
        private static readonly int _maxEuro = 10;
        private EuroStandard()
        {
        }

        private EuroStandard(int value) : this()
        {
            Value = value;
        }

        public static Result<EuroStandard> CreateEuroStandard(int euroStandart)
        {
            return (euroStandart).ToResult()
                .Ensure(x => x > _minEuro, $"{nameof(euroStandart)} must be grater than {_minEuro}.")
                .Ensure(x => x < _maxEuro, $"{nameof(euroStandart)} must be smaller than {_maxEuro}.")
                .Map(x => new EuroStandard(x));
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