using Api.Utils;
using CarDealer.Api.Common;
using CarDealer.Application.ExternalContracts;
using CarDealer.Application.Sale.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDealer.Api.Sale
{
    public class CarController : ApiController
    {
        public CarController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Envelope>> Create(CreateCarCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsSuccess)
                return Ok();
            else
                return Error(result.Errors);
        }
    }
}
