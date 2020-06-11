using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Contracts;
using BodyWeightApp.DataContext.Entities;
using BodyWeightApp.WebApi.Extensions;
using BodyWeightApp.WebApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BodyWeightApp.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ILogger<ProfileController> logger;

        public ProfileController(
            IHttpContextAccessor httpContextAccessor,
            IUserProfileRepository userProfileRepository,
            ILogger<ProfileController> logger)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Returns user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = httpContextAccessor.GetUserId();

            var userProfile = await userProfileRepository.GetUserProfileAsync(userId);
            return userProfile.Match<IActionResult>(
                none: NotFound,
                some: profile =>
                {
                    var model = new UserProfileModel
                    {
                        BirthDate = profile.BirthDate,
                        Height = profile.Height,
                    };
                    return Ok(model);
                }
            );
        }

        /// <summary>
        /// Creates new user profile or updates existing one
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpsertUserProfile([Required] UserProfileModel userProfile)
        {
            var userId = httpContextAccessor.GetUserId();
            var entity = new UserProfile
            {
                BirthDate = userProfile.BirthDate,
                Height = userProfile.Height,
                ID = userId
            };
            await userProfileRepository.UpsertUserProfile(entity);
            return NoContent();
        }
    }
}