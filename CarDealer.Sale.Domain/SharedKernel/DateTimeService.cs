
using CarDealer.Domain.Common;
using System;

namespace CarDealer.Domain.SharedKernel
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
