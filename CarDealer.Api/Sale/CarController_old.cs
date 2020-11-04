using Api.Utils;
using CarDealer.Application.CommonContracts;
using CarDealer.Application.Sale;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarDealer.Api.Sale
{
    public class CarController_old : BaseController
    {
        private readonly ICarService _carService;
        public CarController_old(IUnitOfWork unitOfWork, ICarService carService) : base(unitOfWork)
        {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        }

        [HttpPut("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCarPrice(long availbleCarId, decimal newPrice)
        {
            _carService.ChangeAvailibleCarPrice(availbleCarId, newPrice);
            return Ok();
        }
    }
}
