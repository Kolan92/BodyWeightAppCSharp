using BodyWeightApp.DataContext.Contracts;

using Microsoft.Extensions.DependencyInjection;

namespace BodyWeightApp.DataContext.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterDataContextDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserProfileRepository, UserProfileRepository>();
            serviceCollection.AddTransient<IBodyInfoRepository, BodyInfoRepository>();
        }
    }
}
