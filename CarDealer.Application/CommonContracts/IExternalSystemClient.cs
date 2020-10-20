namespace CarDealer.Application.CommonContracts
{
    public interface IExternalSystemClient
    {
        int GetMileage(string vin);
    }
}
