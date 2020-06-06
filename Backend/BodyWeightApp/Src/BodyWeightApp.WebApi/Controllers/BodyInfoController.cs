using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BodyWeightApp.WebApi.Models;

using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Return list of body measures information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, type: typeof(BodyInfoModel))]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetBodyInfos()
        {
            var height = 1.90;

            var measures = new List<BodyWeightModel>
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
                WeightMeasurements = measures
            };
            return Ok(model);
        }
    }
}