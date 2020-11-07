using Api.Utils;
using CarDealer.Application.CommonContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CarDealer.Api.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IUnitOfWork _unitOfWork;

        public ApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        }

        protected new ActionResult<Envelope> Ok()
        {
            _unitOfWork.Commit();
            return base.Ok(Envelope.Ok());
        }

        protected ActionResult<Envelope> Error(List<string> errorMessages)
        {
            _unitOfWork.Rollback();
            return BadRequest(Envelope.Error(errorMessages));
        }

        protected ActionResult<Envelope<T>> Ok<T>(T result)
        {
            _unitOfWork.Commit();
            return base.Ok(Envelope.Ok(result));
        }

        protected ActionResult<Envelope<T>> Error<T>(List<string> errorMessages)
        {
            _unitOfWork.Rollback();
            return BadRequest(Envelope.Error<T>(errorMessages));
        }
    }
}
