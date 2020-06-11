using System;
using System.Collections.Generic;

namespace BodyWeightApp.DataContext.Entities
{
    public class UserProfile
    {
        public string ID { get; set; }
        public double Height { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public ICollection<BodyWeight> Measurements { get; set; }
    }
}
