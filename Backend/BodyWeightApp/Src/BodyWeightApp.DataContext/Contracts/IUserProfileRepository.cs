using System;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Entities;

using Optional;

namespace BodyWeightApp.DataContext.Contracts
{
    public interface IUserProfileRepository
    {
        Task<Option<UserProfile>> GetUserProfileAsync(string userId);

        Task<Option<UserProfile>> GetUserProfileWithMeasurementsAsync(string userId, DateTimeOffset from, DateTimeOffset till);
        Task UpsertUserProfile(UserProfile userProfile);
    }
}
