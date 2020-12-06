using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            await Task.Yield();
            return this.db.Course.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            await Task.Yield();
            return this.db.Course.Find(id);
        }

        [HttpGet("credit/{credit}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByCredit(int credit)
        {
            await Task.Yield();
            return this.db.Course.Where(c => c.Credits == credit).ToList();
        }

        [HttpGet("credit/{credit}/departmnet/{department}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByCreditAndDepartment(int department, int credit)
        {
            await Task.Yield();
            return this.db.Course.Where(x => x.DepartmentId == department && x.Credits == credit).ToList();
        }

        [HttpPost("")]
        public async Task<ActionResult> PostCourse(Course coures)
        {
            this.db.Course.Add(coures);
            await this.db.SaveChangesAsync();

            return Created($"api/GetCourseById/{coures.CourseId}", coures);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCourse(int id, Course course)
        {
            var c = this.db.Course.Find(id);
            c.Credits = course.Credits;
            c.DepartmentId = course.DepartmentId;

            await this.db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var c = this.db.Course.Find(id);
            this.db.Course.Remove(c);

            await this.db.SaveChangesAsync();
            return Ok(c);
        }
    }
}