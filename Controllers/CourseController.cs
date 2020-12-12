using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.AspNetCore.Http;

namespace ASPNETCore5Demo.Controllers
{
    [Produces("application/json")] // 強迫response皆轉為json
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
        }


        [HttpGet("err")]
        public IActionResult Err()
        {
            throw new Exception("OH");
        }

        [HttpGet("empty")]
        public IActionResult Empty()
        {
            // 若只想回傳字串

            return Ok("TEST"); // 若沒有添加[Produces("application/json")] 會產生string 但browser會序列化失敗
            // 另一種解法
            // return new JsonResult("TEST");
        }

        /// <summary>
        /// 取得課程
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            for (int i = 0; i < 5000; i++)
            {
                await Task.Yield();
            }

            return this.db.Course.ToList();
        }

        /// <summary>
        /// 取得單一課程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            await Task.Yield();
            return this.db.Course.Find(id);
        }

        /// <summary>
        /// 取得課程的學生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/students")]
        public async Task<IEnumerable<VwCourseStudents>> GetCourseStudentsById(int id)
        {
            await Task.Yield();
            return this.db.VwCourseStudents.Where(cs => cs.CourseId == id).ToList();
        }

        /// <summary>
        /// 取得課程的學生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/students/count")]
        public async Task<IEnumerable<VwCourseStudentCount>> GetCourseStudentCountById(int id)
        {
            await Task.Yield();
            return this.db.VwCourseStudentCount.Where(cs => cs.CourseId == id).ToList();
        }

        /// <summary>
        /// 取得課程 By credit
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        [HttpGet("credit/{credit}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByCredit(int credit)
        {
            await Task.Yield();
            return this.db.Course.Where(c => c.Credits == credit).ToList();
        }

        /// <summary>
        /// 取得課程 By 部門、credit
        /// </summary>
        /// <param name="department"></param>
        /// <param name="credit"></param>
        /// <returns></returns>
        [HttpGet("credit/{credit}/departmnet/{department}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByCreditAndDepartment(int department, int credit)
        {
            await Task.Yield();
            return this.db.Course.Where(x => x.DepartmentId == department && x.Credits == credit).ToList();
        }

        /// <summary>
        /// 新增課程
        /// </summary>
        /// <param name="coures"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PostCourse(Course coures)
        {
            this.db.Course.Add(coures);
            await this.db.SaveChangesAsync();

            return Created($"api/GetCourseById/{coures.CourseId}", coures);
        }

        /// <summary>
        /// 更新課程
        /// </summary>
        /// <param name="id"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PutCourse(int id, Course course)
        {
            var c = this.db.Course.Find(id);
            c.Credits = course.Credits;
            c.DepartmentId = course.DepartmentId;

            this.db.Course.Update(c);

            await this.db.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// 刪除課程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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