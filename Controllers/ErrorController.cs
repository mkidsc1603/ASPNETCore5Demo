using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPNETCore5Demo.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        //[Route("/error")]
        //public IActionResult Index()
        //{
        //    return Problem();
        //}

        [Route("/error")]
        public IActionResult Error([FromServices] IHostingEnvironment webHostEnviroment)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature?.Error;
            var isDev = webHostEnviroment.IsDevelopment();
            var problem = new ProblemDetails()
            {
                Detail = isDev ? exception.StackTrace : null,
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = isDev ? $"{exception.GetType().Name}:{exception.Message}" : "An error occurred!",
                Type = exception.GetType().FullName
            };

            //add custom key value into output json result
            problem.Extensions.Add("error", "test");

            return StatusCode(problem.Status.Value, problem);
        }
    }
}
