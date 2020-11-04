using Api.Utils;
using CarDealer.Api.Common;
using CarDealer.Application.CommonContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarDealer.Api.Sale
{
    public class CarController: ApiController
    {
        public CarController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Envelope<int>>> Update(long id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok<int>(1234);
        }

        private ActionResult BadRequest()
        {
            throw new NotImplementedException();
        }
    }
}
