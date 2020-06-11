using System;

namespace BodyWeightApp.DataContext.Entities
{
    public class BodyWeight
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public double Weight { get; set; }
        public DateTimeOffset MeasuredOn { get; set; }
    }
}
