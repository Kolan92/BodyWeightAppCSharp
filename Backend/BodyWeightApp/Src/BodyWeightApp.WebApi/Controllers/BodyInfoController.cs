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
            var measures = new List<BodyWeightModel>
            {
                new BodyWeightModel{Weight = 90, MeasuredOn = new DateTime(2020, 01, 01)},
                new BodyWeightModel{Weight = 90.3, MeasuredOn = new DateTime(2020, 01, 08)},
                new BodyWeightModel{Weight = 91, MeasuredOn = new DateTime(2020, 01, 15)},
                new BodyWeightModel{Weight = 90, MeasuredOn = new DateTime(2020, 01, 22)},
                new BodyWeightModel{Weight = 89.6, MeasuredOn = new DateTime(2020, 01, 29)},
                new BodyWeightModel{Weight = 89, MeasuredOn = new DateTime(2020, 02, 06)},
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