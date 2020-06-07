using System;

namespace BodyWeightApp.DataContext.Entities
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
