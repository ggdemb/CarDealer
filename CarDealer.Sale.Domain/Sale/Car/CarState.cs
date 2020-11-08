using CarDealer.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Domain.Sale.Car
{
    public class CarState : Entity
    {
        //reference data - alternative way:
        //https://enterprisecraftsmanship.com/posts/reference-data-as-code/
        //Remember about integration tests!
        public CarState()
        {

        }
        protected CarState(CarStateEnum enumState)
        {
            Name = enumState.ToString();
            Id = (byte)enumState;
        }
        public new byte Id { get; set; }

        private readonly List<AvailibleCar> _cars;
        public IReadOnlyList<AvailibleCar> Cars { get => _cars.ToList(); }
        public string Name { get; private set; }

        public static implicit operator CarState(CarStateEnum @enum) => new CarState(@enum);

        public static implicit operator CarStateEnum(CarState carState) => (CarStateEnum)carState.Id;
    }
    public enum CarStateEnum : byte
    {
        New = 1,
        Used = 2,
        Broken = 3
    }
}
