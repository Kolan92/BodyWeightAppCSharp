using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Contracts;
using BodyWeightApp.DataContext.Entities;

using Microsoft.EntityFrameworkCore;

namespace BodyWeightApp.DataContext
{
    internal class BodyInfoRepository : IBodyInfoRepository
    {
        private readonly BodyInfoContext dbContext;

        public BodyInfoRepository(BodyInfoContext bodyInfoContext)
        {
            this.dbContext = bodyInfoContext ?? throw new ArgumentNullException(nameof(bodyInfoContext));
        }


        public Task<IEnumerable<BodyWeight>> GetBodyWeightsAsync(string userId, DateTimeOffset from, DateTimeOffset till)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            return Implementation();

            async Task<IEnumerable<BodyWeight>> Implementation()
            {
                return await dbContext.BodyWeights
                    .Where(w => w.UserId == userId)
                    .Where(w => w.MeasuredOn >= from && w.MeasuredOn < till)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public Task AddBodyWeightAsync(BodyWeight bodyWeight)
        {
            if (bodyWeight == null)
                throw new ArgumentNullException(nameof(bodyWeight));

            return Implementation();

            async Task Implementation()
            {
                await dbContext.BodyWeights.AddAsync(bodyWeight);
                await dbContext.SaveChangesAsync();
            }
        }

        public Task DeleteBodyWeightAsync(BodyWeight entity)
        {
            throw new NotImplementedException();
        }
    }
}