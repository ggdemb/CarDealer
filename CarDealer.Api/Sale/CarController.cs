using Api.Utils;
using CarDealer.Persistence;

namespace CarDealer.Api.Sale
{
    public class CarController : BaseController
    {
        public CarController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
