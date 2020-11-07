using CarDealer.Domain.Common;
using MediatR;

namespace CarDealer.Application.Sale.Commands
{
    public class CreateCarCommand : IRequest<Result>
    {
        public int SampleProp { get; set; }
    }
}
