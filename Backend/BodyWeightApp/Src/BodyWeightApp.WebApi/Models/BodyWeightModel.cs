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
            Weight = weight;
            Bmi = weight / (height * height);
            MeasuredOn = measuredOn;
        }

        public double Weight { get; }
        public double Bmi { get; }
        public DateTime MeasuredOn { get; }
    }
}
