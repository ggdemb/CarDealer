using CarDealer.Domain.Common;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class CarName : ValueObject<CarName>
    {
        private CarName()
        {

        }
        public CarName(string brand, string model) : this()
        {
            Brand = brand;
            Model = model;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Brand;
            yield return Model;
        }

        public override string ToString()
        {
            return $"{Model} {Brand}";
        }
    }
}