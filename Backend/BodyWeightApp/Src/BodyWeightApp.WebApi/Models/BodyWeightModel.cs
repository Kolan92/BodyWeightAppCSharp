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
        public double Weight { get; set; }
        public DateTime MeasuredOn { get; set; }
    }
}
