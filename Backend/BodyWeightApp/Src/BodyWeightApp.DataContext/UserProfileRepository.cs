using System;
using System.Linq;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Contracts;
using BodyWeightApp.DataContext.Entities;

using Microsoft.EntityFrameworkCore;

using Optional;

namespace BodyWeightApp.DataContext
{
    internal class UserProfileRepository : IUserProfileRepository
    {
        private readonly BodyInfoContext bodyInfoContext;

        public UserProfileRepository(BodyInfoContext dbContext)
        {
            this.bodyInfoContext = dbContext ?? throw new ArgumentNullException();
        }

        public Task<Option<UserProfile>> GetUserProfileAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Task.FromResult(Option.None<UserProfile>());

            return Implementation();

            async Task<Option<UserProfile>> Implementation()
            {
                var profile = await bodyInfoContext.UserProfiles
                    .SingleOrDefaultAsync(up => up.ID == userId);
                return profile.SomeNotNull();
            }
        }

        public Task<Option<UserProfile>> GetUserProfileWithMeasurementsAsync(string userId, DateTimeOffset @from, DateTimeOffset till)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Task.FromResult(Option.None<UserProfile>());

            return Implementation();

            async Task<Option<UserProfile>> Implementation()
            {
                var profile = await bodyInfoContext.UserProfiles
                    .Include(up => up.Measurements)
                    
                    .SingleOrDefaultAsync(up => up.ID == userId);
                return profile.SomeNotNull();
            }
        }

        public Task UpsertUserProfile(UserProfile userProfile)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            return Implementation();

            async Task Implementation()
            {
                await bodyInfoContext.UserProfiles.Upsert(userProfile)
                    .On(p => p.ID)
                    .WhenMatched(p => new UserProfile
                    {
                        Height = userProfile.Height,
                        BirthDate = userProfile.BirthDate
                    })
                    .RunAsync();

                await bodyInfoContext.SaveChangesAsync();
            }
        }
    }
}
