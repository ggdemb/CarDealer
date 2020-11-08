using CarDealer.Application.Utils;
using CarDealer.Domain.Sale.Car;
using System;

namespace CarDealer.Application.Sale.Common
{
    [CustomSerializationName("TransmissionType")]
    public enum CommandTransmissionType
    {
        Manual,
        Automatic
    }
    [CustomSerializationName("CarType")]
    public enum CommandCarType
    {
        Sport,
        Regular

    }
    [CustomSerializationName("CarState")]
    public enum CommandCarState
    {
        New,
        Used,
        Broken
    }
    static class ApplicationEnumsExtensions
    {
        public static TResult Transform<TSource, TResult>(this TSource inputEnum) where TSource : struct where TResult : struct
        {
            return Enum.Parse<TResult>(inputEnum.ToString());
        }
    }
}
