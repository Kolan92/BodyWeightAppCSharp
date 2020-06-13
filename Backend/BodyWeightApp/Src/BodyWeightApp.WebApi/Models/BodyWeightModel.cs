using System;
using System.Collections.Generic;

namespace BodyWeightApp.WebApi.Models
{
    public class BodyInfoModel
    {
        public double Height { get; set; }
        public IReadOnlyCollection<BodyWeightModel> WeightMeasurements { get; set; }
    }
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

    public class NewBodyWeightMeasurement
    {
        public double Weight { get; set; }
        public DateTimeOffset MeasuredOn { get; set; }
    }
}
