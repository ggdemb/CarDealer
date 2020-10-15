using CarDealer.Domain.Common;
using System;

namespace CarDealer.Domain.Sale.Car
{
    public class CarHistoryItem : Entity
    {
        public DateTime DateOfItem { get; }
        public CarMileage Mileage { get; }
        public string Description { get; }
        public int AvailibleCarId { get; set; }

    }
}
