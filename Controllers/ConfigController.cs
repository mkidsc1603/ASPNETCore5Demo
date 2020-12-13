using ASPNETCore5Demo.Models.Custom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo.Controllers
{
    [Route("config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IOptions<JwtSetting> jwtSetting { get; set; }
        private ILogger<ConfigController> logger { get; set; }

        public ConfigController(IOptions<JwtSetting> jwtSetting, ILogger<ConfigController> logger)
        {
            this.jwtSetting = jwtSetting;
            this.logger = logger;
        }

        [HttpGet("")]
        public ActionResult<JwtSetting> Get()
        {
            logger.LogTrace("Trace {ID}", "Andy");
            logger.LogDebug("Debug {ID}", "Andy");
            logger.LogInformation("Info {ID}", "Andy");
            logger.LogWarning("warning {ID}", "Andy");
            logger.LogError("Error {ID}", "Andy");
            logger.LogCritical("Critical {ID}", "Andy");

            return this.jwtSetting.Value;
        }
    }
}
