using CarDealer.Domain.Common;
using System;

namespace CarDealer.Domain.Sale.Car
{
    public class CarHistoryItem : Entity
    {
        public DateTime DateOfItem { get; private set; }
        public CarMileage Mileage { get; private set; }
        public string Description { get; private set; }
        public long AvailibleCarId { get; private set; }
    }
}
