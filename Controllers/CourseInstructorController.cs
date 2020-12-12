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
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseInstructorController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 取得課程導師
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CourseInstructor> Get()
        {
            return db.CourseInstructor.ToList();
        }

        /// <summary>
        /// 取得課程導師 By 課程id、指導者id
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="InstructorId"></param>
        /// <returns></returns>
        [HttpGet("/course/{CourseId?}/instructor/{InstructorId?}")]
        public IEnumerable<CourseInstructor> Get(int? CourseId, int? InstructorId)
        {
            var query = this.db.CourseInstructor.AsQueryable();
            if (CourseId.HasValue)
                query = query.Where(c => c.CourseId.Equals(CourseId));
            if (InstructorId.HasValue)
                query = query.Where(c => c.InstructorId.Equals(InstructorId));

            return query.ToList();
        }

        /// <summary>
        /// 新增課程導師
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] CourseInstructor model)
        {
            var IsExist = this.db.CourseInstructor.Any(x => x.CourseId.Equals(model.CourseId) && x.InstructorId.Equals(model.InstructorId));

            if (IsExist)
            {
                return Created($"api/CourseInstructor/course/{model.CourseId}/instructor/{model.InstructorId}", "already exist!");
            }
            else
            {
                this.db.CourseInstructor.Add(model);
                this.db.SaveChanges();
            }

            return Created($"api/CourseInstructor/course/{model.CourseId}/instructor/{model.InstructorId}", model);
        }

        /// <summary>
        /// 更新課程導師
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="InstructorId"></param>
        /// <returns></returns>
        [HttpPut("/course/{CourseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Put(int CourseId, [FromBody] int InstructorId)
        {
            var item = this.db.CourseInstructor.Where(c => c.CourseId.Equals(CourseId)).FirstOrDefault();
            if (item != null)
            {
                item.InstructorId = InstructorId;
                this.db.CourseInstructor.Update(item);
                this.db.SaveChanges();
            }
            return NoContent();
        }

        /// <summary>
        /// 刪除課程導師
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="InstructorId"></param>
        /// <returns></returns>
        [HttpDelete("/course/{CourseId}/instructor/{InstructorId}")]
        public IActionResult Delete(int CourseId, int InstructorId)
        {
            var item = this.db.CourseInstructor.Where(c => c.CourseId.Equals(CourseId) && c.InstructorId.Equals(InstructorId)).FirstOrDefault();
            if (item != null)
            {
                this.db.CourseInstructor.Remove(item);
                this.db.SaveChanges();
            }

            return Ok(item);
        }
    }
}
