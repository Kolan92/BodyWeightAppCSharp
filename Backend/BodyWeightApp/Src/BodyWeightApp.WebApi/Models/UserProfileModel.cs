using System;

namespace BodyWeightApp.WebApi.Models
{
    public class UserProfileModel
    {
        public double Height { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }
}
