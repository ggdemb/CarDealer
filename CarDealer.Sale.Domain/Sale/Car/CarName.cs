using CarDealer.Domain.Common;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class CarName : ValueObject<CarName>
    {
        private CarName()
        {

        }
        private CarName((string brand, string model) parameters) : this()
        {
            Brand = parameters.brand;
            Model = parameters.model;
        }
        public static Result<CarName> Create(string brand, string model)
        {
            
            return (brand, model).ToResult()
                .Ensure(x => string.IsNullOrEmpty(x.brand), "Brand name cannot be empty")
                .Ensure(x => string.IsNullOrEmpty(x.model), "Brand model cannot be empty")
                .Ensure(x => x.brand.Length > 50, "Brand name cannot be longer than 50 characters")
                .Ensure(x => x.model.Length > 55, "Model name cannot be longer than 55 characters")
                .Map(x => new CarName(x));
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