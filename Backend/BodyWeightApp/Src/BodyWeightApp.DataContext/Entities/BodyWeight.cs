using System;

namespace BodyWeightApp.DataContext.Entities
{
    public class BodyWeight
    {
        public string UserId { get; set; }
        public double Weight { get; set; }
        public DateTime MeasuredOn { get; set; }
    }
}
