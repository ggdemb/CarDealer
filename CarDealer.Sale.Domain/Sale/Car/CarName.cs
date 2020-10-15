using CarDealer.Domain.Common;
using System.Collections.Generic;

namespace CarDealer.Domain.Sale.Car
{
    public class CarName : ValueObject<CarName>
    {
        private CarName()
        {

        }
        public CarName(string brandName, string modelName) : this()
        {
            BrandName = brandName;
            ModelName = modelName;
        }

        public string BrandName { get; private set; }
        public string ModelName { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BrandName;
            yield return ModelName;
        }

        public override string ToString()
        {
            return $"{ModelName} {BrandName}";
        }
    }
}