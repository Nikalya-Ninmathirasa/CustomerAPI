using CustomerAPI.Data;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CusDbContext _dbContext;

        public CustomerController(CusDbContext dbContext) {

            _dbContext = dbContext;
        }

        //Get
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult Get()
        {
            var customers = _dbContext.Customer.ToList();
            return Ok(customers);
        }

        //Get by Id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route ("GetById")]
        public ActionResult Get(int id)
        {
            var customer = _dbContext.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound($"Provided id is not found {id}");
            }
            return Ok(customer);
        }

        //Post
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Post([FromBody]Customer customer)
        {
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();
            return Ok("Created Successfully!");
        }

        //update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public ActionResult Update([FromBody] Customer customer)
        {
            _dbContext.Customer.Update(customer);
            _dbContext.SaveChanges();
            return NoContent();
        }

        //Delete
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest("Should be put an id");
            }

            var customer = _dbContext.Customer.FirstOrDefault(x=>x.Id==id);
            if(customer == null)
            {
                return NotFound($"Provided id is not found {id}");
            }

            _dbContext.Customer.Remove(customer);
            _dbContext.SaveChanges();
            return NoContent();
        }


    }
}
