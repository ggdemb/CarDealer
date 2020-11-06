using CarDealer.Domain.Common;

namespace CarDealer.Domain.Sale.Car
{
    public class CarState : Entity
    {
        //reference data - remember about INTERGATION TESTS. It may be considered as breaking OCP principle.
        //https://enterprisecraftsmanship.com/posts/reference-data-as-code/

        public static CarState New = new CarState(1, "New");
        public static CarState Used = new CarState(2, "Used");
        public static CarState Broken = new CarState(3, "Broken");
        public CarState(long id, string name)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; }
        public static Result<CarState> GetCarState(Maybe<string> name)
        {
            if (name.HasNoValue)
                return Result.Fail<CarState>($"CarState {nameof(name)} is not specified");
            if (name.Value == New.Name)
                return Result.Ok(New);
            if (name.Value == Used.Name)
                return Result.Ok(Used);
            if (name.Value == Broken.Name)
                return Result.Ok(Broken);
            return Result.Fail<CarState>($"CarState {nameof(name)} is invalid");
        }
    }
}
