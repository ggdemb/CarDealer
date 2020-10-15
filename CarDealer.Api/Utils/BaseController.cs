using CarDealer.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Api.Utils
{
    public class BaseController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public BaseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected new IActionResult Ok()
        {
            _unitOfWork.Commit();
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            _unitOfWork.Commit();
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult Error(string errorMessage)
        {
            _unitOfWork.Rollback();
            return BadRequest(Envelope.Error(errorMessage));
        }
    }
}
