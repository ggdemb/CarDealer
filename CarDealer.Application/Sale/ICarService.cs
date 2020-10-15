namespace CarDealer.Application.Sale
{
    public interface ICarService
    {
        void ChangeAvailibleCarPrice(long availbleCarId, decimal newPriceInPln);
    }
}
