using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCore5Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public DepartmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 眔场
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            // TODO: Your code here
            await Task.Yield();

            return this.db.Department.ToList();
        }

        /// <summary>
        /// 眔虫场
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetDepartmentCourses(int id)
        {
            await Task.Yield();

            // return this.db.Department.Include(p => p.Course)
            //     .First(p => p.DepartmentId == id).Course.ToList();

            return this.db.Course.Where(c => c.DepartmentId == id).ToList();
        }

        /// <summary>
        /// 眔场揭祘计
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/course/count")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCoursesCount(int id)
        {
            await Task.Yield();
            return this.db.VwDepartmentCourseCount
                .FromSqlRaw($"SELECT * FROM VwDepartmentCourseCount WHERE DepartmentId = {id}")
                .ToList();
        }

        /// <summary>
        /// 穝糤场
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<Department>> PostDeparmtnet(Department model)
        {
            // TODO: Your code here
            await Task.Yield();
            this.db.Department.Add(model);

            await this.db.SaveChangesAsync();
            return Created("api/GetDepartmentById/" + model.DepartmentId, model);
        }

        /// <summary>
        /// 穝场
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department model)
        {
            // TODO: Your code here
            await Task.Yield();
            var d = this.db.Department.Find(id);
            d.Name = model.Name;

            this.db.Department.Update(d);
            await this.db.SaveChangesAsync();
            return Ok(d);
        }

        /// <summary>
        /// 埃场
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentlById(int id)
        {
            // TODO: Your code here
            await Task.Yield();

            var d = this.db.Department.Find(id);

            this.db.Department.Remove(d);

            await this.db.SaveChangesAsync();
            return Ok(d);
        }
    }
}