using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;

namespace CarDealer.Domain.SharedKernel
{
    public class Pln : ValueObject<Pln>
    {
        private readonly string _currencySymbol = "PLN";

        public Pln(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException($"{amount} must be grater than zero.");
            Amount = amount;
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