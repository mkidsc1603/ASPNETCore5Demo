using ASPNETCore5Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return this.db.Person.ToList();
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return this.db.Person.Find(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person model)
        {
            this.db.Person.Add(model);
            this.db.SaveChanges();

            return Created($"api/Person/{model.Id}", model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person model)
        {
            var item = this.db.Person.Find(id);
            if (item != null)
            {
                item.LastName = model.LastName;
                item.FirstName = model.FirstName;
                item.HireDate = model.HireDate;
                item.EnrollmentDate = model.EnrollmentDate;
                item.Discriminator = model.Discriminator;

                this.db.Person.Update(item);
                this.db.SaveChanges();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.db.Person.Find(id);
            if (item != null)
            {
                this.db.Person.Remove(item);
                this.db.SaveChanges();
            }
            return Ok(item);
        }
    }
}
