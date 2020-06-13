using System;
using System.Collections.Generic;

namespace BodyWeightApp.WebApi.Models
{
    public class BodyInfoModel
    {

        public BodyInfoModel(double height, DateTimeOffset birthDate, IReadOnlyCollection<BodyWeightModel> weightMeasurements)
        {
            Height = height;
            BirthDate = birthDate;
            WeightMeasurements = weightMeasurements;
        }

        public double Height { get; }
        public DateTimeOffset BirthDate { get; }
        public IReadOnlyCollection<BodyWeightModel> WeightMeasurements { get; }
    }
}
