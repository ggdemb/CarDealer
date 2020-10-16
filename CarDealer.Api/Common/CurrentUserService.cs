using CarDealer.Application.CommonContracts;

namespace CarDealer.Api.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        public string FullName => "Tomasz Kowalski";
        public string Login => "ABCDE/tkowal";
    }
}
