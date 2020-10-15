using CarDealer.Common.Domain;
using System.Collections.Generic;

namespace CarDealer.Sale.Domain.Car
{
    public class TaxInformation : ValueObject<TaxInformation>
    {
        public TaxInformation()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}