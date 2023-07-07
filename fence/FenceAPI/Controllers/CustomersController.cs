using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FenceWebApp.Models;
using FenceWebApp.Data;

namespace FenceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersDbContext _dbContext;

        public CustomersController(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _dbContext.Customers.ToList();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = _dbContext.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.firstName = updatedCustomer.firstName;
            existingCustomer.lastName = updatedCustomer.lastName;

            _dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _dbContext.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _dbContext.Customers.Remove(existingCustomer);
            _dbContext.SaveChanges();

            return NoContent();
        }
        
        // get API URL
        [HttpGet]
        public ActionResult<string> GetApiUrl()
        {
            var apiUrl = $"{Request.Scheme}://{Request.Host}";
            return apiUrl;
        }

    }
}
