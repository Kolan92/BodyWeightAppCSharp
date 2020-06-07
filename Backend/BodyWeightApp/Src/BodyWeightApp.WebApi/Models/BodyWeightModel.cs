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
        public BodyWeightModel(double weight, double height, DateTime measuredOn)
        {
            var heightInMeters = height / 100;
            Weight = weight;
            Bmi = weight / (heightInMeters * heightInMeters);
            MeasuredOn = measuredOn;
        }

        public double Weight { get; set; }
        public double Bmi { get; set; }
        public DateTime MeasuredOn { get; set; }
    }
}
