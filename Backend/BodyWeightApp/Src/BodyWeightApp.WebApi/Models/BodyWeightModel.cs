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
        public BodyWeightModel(double weight, double height, DateTimeOffset measuredOn)
        {
            var heightInMeters = height / 100;
            Weight = weight;
            Bmi = weight / (heightInMeters * heightInMeters);
            MeasuredOn = measuredOn;
        }

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
