using CarDealer.Application.Sale.Common;
using CarDealer.Application.Utils;
using CarDealer.Domain.Common;
using CarDealer.Domain.Sale.Car;
using MediatR;
using System;

namespace CarDealer.Application.Sale.Commands
{
    [CustomSerializationName("Car")]
    public class CreateCarCommand : IRequest<Result>
    {
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public int EuroStandart { get; set; }
        public decimal? EngineDisplacementInCm3 { get; set; }
        public decimal? BatteryCapacityInKwh { get; set; }
        public CommandTransmissionType TransmissionType { get; set; }
        public int MileageInKm { get; set; }
        public decimal PriceInPln { get; set; }
        public CommandCarType CarType { get; set; }
        public CommandEngineType EngineType { get; set; }
        public CommandCarState State { get; set; }

    }    
}