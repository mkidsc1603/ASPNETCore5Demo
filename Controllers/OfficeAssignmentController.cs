using ASPNETCore5Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeAssignmentController : ControllerBase
    {

        private readonly ContosoUniversityContext db;
        public OfficeAssignmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<OfficeAssignment> Get()
        {
            return this.db.OfficeAssignment.ToList();
        }

        [HttpGet("{id}")]
        public OfficeAssignment Get(int id)
        {
            return this.db.OfficeAssignment.Find(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] OfficeAssignment model)
        {
            if (this.db.OfficeAssignment.Any(o => o.InstructorId.Equals(model.InstructorId)))
            {
                return Created($"api/OfficeAssignment/{model.InstructorId}", "already exist");
            }
            else
            {
                this.db.OfficeAssignment.Add(model);
                this.db.SaveChanges();

                return Created($"api/OfficeAssignment/{model.InstructorId}", model);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OfficeAssignment model)
        {
            var item = this.db.OfficeAssignment.Find(id);
            if (item != null)
            {
                item.Location = model.Location;
                this.db.OfficeAssignment.Update(item);

                this.db.SaveChanges();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.db.OfficeAssignment.Find(id);
            if (item != null)
            {
                this.db.OfficeAssignment.Remove(item);
                this.db.SaveChanges();
            }

            return Ok(item);
        }
    }
}
