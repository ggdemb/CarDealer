namespace CarDealer.Application.ExternalContracts
{
    public interface IExternalSystemClient
    {
        int GetMileage(string vin);
    }
}
