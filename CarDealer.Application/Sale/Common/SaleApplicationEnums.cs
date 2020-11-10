using CarDealer.Application.Utils;
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
    [CustomSerializationName("EngineType")]
    public enum CommandEngineType
    {
        Diesel,
        Petrol,
        Hybrid,
        FullyElectric
    }
    internal static class ApplicationEnumsExtensions
    {
        internal static TResult ToEquivalent<TSource, TResult>(this TSource inputEnum) where TSource : struct where TResult : struct
        {
            return Enum.Parse<TResult>(inputEnum.ToString());
        }
    }
}
