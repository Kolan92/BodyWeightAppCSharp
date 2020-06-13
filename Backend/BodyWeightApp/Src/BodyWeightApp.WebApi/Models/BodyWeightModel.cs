using System;

namespace BodyWeightApp.WebApi.Models
{
    public class BodyWeightModel
    {
        public BodyWeightModel(int id, double weight, DateTimeOffset measuredOn, double? height = null)
        {
            Id = id;
            Weight = weight;
            MeasuredOn = measuredOn;
            if (height is double heightValue)
            {
                var heightInMeters = heightValue / 100;
                Bmi = weight / (heightInMeters * heightInMeters);
            }
        }

        public int Id { get; }
        public double Weight { get; }
        public double Bmi { get; }
        public DateTimeOffset MeasuredOn { get; }
    }
}
