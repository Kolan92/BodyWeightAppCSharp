using System;
using System.Text;

namespace BodyWeightApp.WebApi.Models
{
    public class NewBodyWeightMeasurement
    {
        private const int MaxWeight = 1000;
        private const int MinWeight = 1;
        public double Weight { get; set; }
        public DateTimeOffset MeasuredOn { get; set; }

        public (bool isValid, string reason) IsValid()
        {
            var messageBuilder = new StringBuilder();
            if (Weight < MinWeight || Weight > MaxWeight)
                messageBuilder.AppendLine($"Weight must be between {MinWeight} and {MaxWeight}");

            if (MeasuredOn > DateTimeOffset.UtcNow)
                messageBuilder.AppendLine("Measure date must be less than now");

            var reason = messageBuilder.ToString();
            return (string.IsNullOrWhiteSpace(reason), reason);
        }
    }
}