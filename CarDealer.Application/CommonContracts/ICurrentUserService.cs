namespace CarDealer.Application.CommonContracts
{
    public interface ICurrentUserService
    {
        string FullName { get; }
        string Login { get; }
    }
}
