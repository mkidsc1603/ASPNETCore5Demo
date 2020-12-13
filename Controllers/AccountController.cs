using ASPNETCore5Demo.Helpers;
using ASPNETCore5Demo.Models;
using ASPNETCore5Demo.Models.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtHelpers jwt;
        public AccountController(JwtHelpers jwt)
        {
            this.jwt = jwt;
        }

        [HttpPost("~/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<OUT_LoginModel> Post(IN_LoginModel model)
        {
            if (ValidateUser(model))
            {
                return new OUT_LoginModel()
                {
                    Token = jwt.GenerateToken(model.Username, 10)
                };

            }
            else
            {
                return BadRequest();
            }
        }

        private bool ValidateUser(IN_LoginModel model)
        {
            return true;
        }
    }
}
