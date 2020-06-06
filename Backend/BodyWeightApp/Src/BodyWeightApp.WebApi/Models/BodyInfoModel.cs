using System;

namespace BodyWeightApp.WebApi.Models
{
    public class BodyInfoModel
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime MeasuredOn { get; set; }
    }
}
