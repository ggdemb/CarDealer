namespace CarDealer.Application.ExternalContracts
{
    public interface ICurrentUserService
    {
        string FullName { get; }
        string Login { get; }
    }
}
