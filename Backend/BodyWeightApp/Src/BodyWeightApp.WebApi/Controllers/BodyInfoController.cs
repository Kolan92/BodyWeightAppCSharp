using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> GetBodyInfo()
        {
            var userId = httpContextAccessor.GetUserId();

            var height = 190;

            var measurements = new List<BodyWeightModel>
            {
                new BodyWeightModel( 90, height, new DateTime(2020, 01, 01)),
                new BodyWeightModel( 90.3, height, new DateTime(2020, 01, 08)),
                new BodyWeightModel( 91, height, new DateTime(2020, 01, 15)),
                new BodyWeightModel( 90, height, new DateTime(2020, 01, 22)),
                new BodyWeightModel( 89.6, height, new DateTime(2020, 01, 29)),
                new BodyWeightModel( 100, height, new DateTime(2020, 02, 06))
            };

            var model = new BodyInfoModel
            {
                Height = 190,
                WeightMeasurements = measurements
            };
            return Ok(model);
        }

        /// <summary>
        /// Adds new measurements to the database
        /// </summary>
        /// <param name="bodyWeightModel"></param>
        /// <returns></returns>

        [HttpPost]
        [SwaggerResponse(200)]
        public async Task<IActionResult> AddBodyWeight([Required] BodyWeightModel bodyWeightModel)
        {
            var userId = httpContextAccessor.GetUserId();

            var entity = new BodyWeight
            {
                UserId = userId,
                Weight = bodyWeightModel.Weight,
                MeasuredOn = bodyWeightModel.MeasuredOn
            };
            await bodyInfoRepository.AddBodyWeightAsync(entity);
            return NoContent();
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
                UserId = userId,
                Weight = bodyWeightModel.Weight,
                MeasuredOn = bodyWeightModel.MeasuredOn
            };
            await bodyInfoRepository.DeleteBodyWeightAsync(entity);
            return NoContent();
        }
    }
}