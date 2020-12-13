using ASPNETCore5Demo.Models.Custom;
using Microsoft.AspNetCore.Mvc;
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

        public ConfigController(IOptions<JwtSetting> jwtSetting)
        {
            this.jwtSetting = jwtSetting;
        }

        [HttpGet("")]
        public ActionResult<JwtSetting> Get()
        {
            return this.jwtSetting.Value;
        }
    }
}
