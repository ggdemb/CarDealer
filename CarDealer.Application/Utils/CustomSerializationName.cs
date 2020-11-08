using System;

namespace CarDealer.Application.Utils
{
    public class CustomSerializationName: Attribute
    {
        public CustomSerializationName(string customName)
        {
            if (string.IsNullOrEmpty(customName))
            {
                throw new ArgumentException("Custom name cannot be null or empty", nameof(customName));
            }

            CustomName = customName;
        }

        public string CustomName { get; }
    }
}
