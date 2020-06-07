using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Contracts;
using BodyWeightApp.DataContext.Entities;

using Optional;

namespace BodyWeightApp.DataContext
{
    internal class UserProfileRepository : IUserProfileRepository
    {
        private static readonly ConcurrentDictionary<string, UserProfile> profiles =
            new ConcurrentDictionary<string, UserProfile>();

        public Task<Option<UserProfile>> GetUserProfileAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Task.FromResult(Option.None<UserProfile>());

            profiles.TryGetValue(userId, out var profile);
            return Task.FromResult(profile.SomeNotNull());
        }

        public Task UpsertUserProfile(UserProfile userUserProfile)
        {
            if (userUserProfile == null)
                throw new ArgumentNullException(nameof(userUserProfile));

            profiles.AddOrUpdate(userUserProfile.UserId, userUserProfile, (uid, profile) => userUserProfile);

            return Task.CompletedTask;
        }
    }
}
