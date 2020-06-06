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
        [SwaggerResponse(200, type: typeof(IEnumerable<BodyInfoModel>))]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetBodyInfos()
        {
            var models = new List<BodyInfoModel>
            {
                new BodyInfoModel{Height = 190, Weight = 90, MeasuredOn = new DateTime(2020, 01, 01)},
                new BodyInfoModel{Height = 190, Weight = 90.3, MeasuredOn = new DateTime(2020, 01, 08)},
                new BodyInfoModel{Height = 190, Weight = 91, MeasuredOn = new DateTime(2020, 01, 15)},
                new BodyInfoModel{Height = 190, Weight = 90, MeasuredOn = new DateTime(2020, 01, 22)},
                new BodyInfoModel{Height = 190, Weight = 89.6, MeasuredOn = new DateTime(2020, 01, 29)},
                new BodyInfoModel{Height = 190, Weight = 89, MeasuredOn = new DateTime(2020, 02, 06)},
            };

            return Ok(models);
        }
    }
}