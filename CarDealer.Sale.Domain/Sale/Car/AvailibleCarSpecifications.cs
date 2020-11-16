using CarDealer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CarDealer.Domain.Sale.Car
{
    public class ElectricCarSpecification : Specification<AvailibleCar>
    {
        private readonly IReadOnlyCollection<EngineType> _electricEnginesTypes = new List<EngineType>() { EngineType.FullyElectric, EngineType.Hybrid };

        public override Expression<Func<AvailibleCar, bool>> ToExpression()
        {
            return movie => _electricEnginesTypes.Any(x => x == movie.Engine.Type) && movie.Engine.BatteryCapacity != BatteryCapacity.NonElectricOrHybridBatteryCapacity;
        }
    }

    public class CarEuroNormAtMostSpecification : Specification<AvailibleCar>
    {
        private readonly EuroStandard _minimalEuroNorm;

        public CarEuroNormAtMostSpecification(EuroStandard atLeastNorm)
        {
            _minimalEuroNorm = atLeastNorm;
        }

        public override Expression<Func<AvailibleCar, bool>> ToExpression()
        {
            return movie => movie.Engine.EuroStandard >= _minimalEuroNorm;
        }
    }

    public class NotFixedCarPriceSpecification : Specification<AvailibleCar>
    {
        public override Expression<Func<AvailibleCar, bool>> ToExpression()
        {
            return car => car.IsNotReserved;
        }
    }
}
