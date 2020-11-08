using CarDealer.Application.ExternalContracts;
using CarDealer.Domain.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealer.Application.Sale.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Result>
    {
        private readonly ICarDealerContext _dbContext;

        public CreateCarCommandHandler(ICarDealerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Result> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            //TODO: here you can orchestrate domain actions
            return await Task.FromResult(Result.Ok());
        }
    }
}