using ASPNETCore5Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;

        public EnrollmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Enrollment> Get()
        {
            return this.db.Enrollment.ToList();
        }

        [HttpGet("{id}")]
        public Enrollment Get(int id)
        {
            return this.db.Enrollment.Find(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] Enrollment model)
        {
            Enrollment post = new Enrollment()
            {
                CourseId = model.CourseId,
                StudentId = model.StudentId,
                Grade = model.Grade
            };

            this.db.Enrollment.Add(post);
            this.db.SaveChanges();
            return Created($"api/Enrollment/{post.EnrollmentId}", post);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Put(int id, [FromBody] Enrollment model)
        {
            var item = this.db.Enrollment.Find(id);
            if (item != null)
            {
                item.CourseId = model.CourseId;
                item.StudentId = model.StudentId;
                item.Grade = model.Grade;

                this.db.Enrollment.Update(item);
                this.db.SaveChanges();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.db.Enrollment.Find(id);
            if (item != null)
                this.db.Enrollment.Remove(item);

            return Ok(item);
        }
    }
}
