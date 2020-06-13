using System;
using System.Text;

namespace BodyWeightApp.WebApi.Models
{
    public class UserProfileModel
    {
        private const double MinHeight = 20;
        private const double MaxHeight = 300;
        private const int MaxAge = 136;
        public double Height { get; set; }
        public DateTimeOffset BirthDate { get; set; }

        public (bool isValid, string reason) IsValid()
        {
            var messageBuilder = new StringBuilder();
            if (Height < MinHeight || Height > MaxHeight)
                messageBuilder.AppendLine($"Height must be between {MinHeight} and {MaxHeight}");
            var now = DateTimeOffset.UtcNow;
            if (BirthDate < now.AddYears(-MaxAge) || BirthDate > now)
                messageBuilder.AppendLine("Birth date must be between now and 135 years ago");

            var reason = messageBuilder.ToString();
            return (string.IsNullOrWhiteSpace(reason), reason);
        }
    }
}
