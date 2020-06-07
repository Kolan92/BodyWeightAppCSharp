using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Contracts;
using BodyWeightApp.DataContext.Entities;

namespace BodyWeightApp.DataContext
{
    internal class BodyInfoRepository : IBodyInfoRepository
    {
        private static readonly ConcurrentDictionary<string, List<BodyWeight>> bodyWeightsDict =
            new ConcurrentDictionary<string, List<BodyWeight>>();

        public Task<IEnumerable<BodyWeight>> GetBodyWeightsAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Task.FromResult(Enumerable.Empty<BodyWeight>());

            bodyWeightsDict.TryGetValue(userId, out var bodyWeights);
            return Task.FromResult(bodyWeights ?? Enumerable.Empty<BodyWeight>());
        }

        public Task AddBodyWeightAsync(BodyWeight bodyWeight)
        {
            if (bodyWeight == null)
                throw new ArgumentNullException(nameof(bodyWeight));

            bodyWeightsDict.AddOrUpdate(bodyWeight.UserId, new List<BodyWeight> { bodyWeight },
            (uid, weights) =>
               {
                   weights.Add(bodyWeight);
                   return weights;
               });

            return Task.CompletedTask;
        }

        public Task DeleteBodyWeightAsync(BodyWeight entity)
        {
            throw new NotImplementedException();
        }
    }
}