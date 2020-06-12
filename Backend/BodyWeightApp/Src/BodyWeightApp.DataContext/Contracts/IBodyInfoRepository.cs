using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Entities;
using Optional;

namespace BodyWeightApp.DataContext.Contracts
{
    public interface IBodyInfoRepository
    {
        Task<IEnumerable<BodyWeight>> GetBodyWeightsAsync(string userId, DateTimeOffset @from, DateTimeOffset till);
        Task AddBodyWeightAsync(BodyWeight bodyWeight);
        Task DeleteBodyWeightAsync(BodyWeight entity);
    }
}