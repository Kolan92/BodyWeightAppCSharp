using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace BodyWeightApp.WebApi.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        private const string UserIdClaimName = "uid";

        public static string GetUserId(this IHttpContextAccessor httpContextAccessor) =>
            httpContextAccessor.HttpContext.User
                .FindFirstValue(UserIdClaimName);
    }
}
