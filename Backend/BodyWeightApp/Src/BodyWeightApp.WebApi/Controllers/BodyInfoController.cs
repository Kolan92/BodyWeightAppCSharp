using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using BodyWeightApp.DataContext.Contracts;
using BodyWeightApp.DataContext.Entities;
using BodyWeightApp.WebApi.Extensions;
using BodyWeightApp.WebApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.Swagger.Annotations;

namespace BodyWeightApp.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class BodyInfoController : ControllerBase
    {
        private readonly IBodyInfoRepository bodyInfoRepository;
        private readonly IUserProfileRepository profileRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BodyInfoController(
            IBodyInfoRepository bodyInfoRepository,
            IUserProfileRepository profileRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.bodyInfoRepository = bodyInfoRepository ?? throw new ArgumentNullException(nameof(bodyInfoRepository));
            this.profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Return info with  list of body measurements information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, type: typeof(BodyInfoModel))]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetBodyInfo(DateTimeOffset? from, DateTimeOffset? till)
        {
            var (fromValue, tillValue) = ParseDateRange();
            if (fromValue >= tillValue)
                return BadRequest("Till date must be greater than from date");

            var userId = httpContextAccessor.GetUserId();

            var userProfile = profileRepository.GetUserProfileAsync(userId);
            var measurements = bodyInfoRepository.GetBodyWeightsAsync(userId, fromValue, tillValue);
            await Task.WhenAll(userProfile, measurements);

            return userProfile.Result.Match<IActionResult>(
                none: () => NotFound("Can't find user profile"),
                some: profile =>
                {
                    var model = new BodyInfoModel
                    {
                        Height = profile.Height,
                        WeightMeasurements = measurements.Result
                            .Select(m => new BodyWeightModel(m.ID, m.Weight, m.MeasuredOn, profile.Height))
                            .ToList()
                    };

                    return Ok(model);
                }
            );

            (DateTimeOffset fromValue, DateTimeOffset tillValue) ParseDateRange()
            {
                var tillValue = till ?? DateTimeOffset.UtcNow;
                var fromValue = from ?? tillValue.AddYears(-1);

                return (fromValue, tillValue);
            }
        }

        /// <summary>
        /// Adds new measurements to the database
        /// </summary>
        /// <param name="bodyWeightModel"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(200)]
        public async Task<IActionResult> AddBodyWeight([Required] NewBodyWeightMeasurement bodyWeightModel)
        {
            var userId = httpContextAccessor.GetUserId();

            var entity = new BodyWeight
            {
                UserId = userId,
                Weight = bodyWeightModel.Weight,
                MeasuredOn = bodyWeightModel.MeasuredOn
            };
            await bodyInfoRepository.AddBodyWeightAsync(entity);
            var model = new BodyWeightModel(entity.ID, entity.Weight, entity.MeasuredOn);
            return Ok(model);
        }

        /// <summary>
        /// Deletes measurements from the database
        /// </summary>
        /// <param name="bodyWeightModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse(200)]
        public async Task<IActionResult> DeleteBodyWeight([Required] BodyWeightModel bodyWeightModel)
        {
            var userId = httpContextAccessor.GetUserId();

            var entity = new BodyWeight
            {
                ID = bodyWeightModel.Id,
                UserId = userId,
                Weight = bodyWeightModel.Weight,
                MeasuredOn = bodyWeightModel.MeasuredOn
            };
            await bodyInfoRepository.DeleteBodyWeightAsync(entity);
            return NoContent();
        }
    }
}