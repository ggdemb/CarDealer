using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.SharedKernel
{
    public class Pln : ValueObject<Pln>
    {
        private readonly string _currencySymbol = "PLN";
        private static readonly decimal _minimalPrice = 0.0M;

        private Pln(decimal amount)
        {
            Amount = amount;
        }
        public static Result<Pln> Create(decimal amount)
        {
            return (amount).ToResult()
                .Ensure(amount => amount > _minimalPrice, $"{nameof(amount)} must be grater than {_minimalPrice}.")
                .Map(amount => new Pln(amount));
        }
        public decimal Amount { get; private set; }

        public override string ToString()
        {
            return $"{Amount} {_currencySymbol}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }


    }
}