using CarDealer.Application.ExternalContracts;
using CarDealer.Application.Sale.Common;
using CarDealer.Domain.Common;
using CarDealer.Domain.Sale.Car;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealer.Application.Sale.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Result>
    {
        private readonly ICarDealerContext _dbContext;
        private readonly ICarFactory _carFactory;

        public CreateCarCommandHandler(ICarDealerContext dbContext, ICarFactory carFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _carFactory = carFactory ?? throw new ArgumentNullException(nameof(carFactory));
        }

        public async Task<Result> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var carResult = _carFactory.CreateCar(
                request.BrandName,
                request.ModelName,
                request.EngineType.ToEquivalent<CommandEngineType, EngineType>(),
                request.EuroStandart,
                request.EngineDisplacementInCm3,
                request.BatteryCapacityInKwh,
                request.TransmissionType.ToEquivalent<CommandTransmissionType, TransmissionType>(),
                request.MileageInKm,
                request.PriceInPln,
                request.CarType.ToEquivalent<CommandCarType, CarType>(),
                request.State.ToEquivalent<CommandCarState, CarStateEnum>());

            //TODO: store into db using repository.

            if (carResult.IsFailure)            
                return await Task.FromResult(Result.Fail(carResult.Errors));

            
            return await Task.FromResult(Result.Ok());
        }
    }
}