using System.Collections.Generic;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Entities;

namespace BodyWeightApp.DataContext.Contracts
{
    public interface IBodyInfoRepository
    {
        Task<IEnumerable<BodyWeight>> GetBodyWeightsAsync(string userId);
        Task AddBodyWeightAsync(BodyWeight bodyWeight);
        Task DeleteBodyWeightAsync(BodyWeight entity);
    }
}