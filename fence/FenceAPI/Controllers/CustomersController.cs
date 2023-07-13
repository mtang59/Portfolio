using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CustomersController> _logger; // print messages to terminal

        public CustomersController(CustomersDbContext dbContext, ILogger<CustomersController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET: api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _dbContext.Customers.Where(c => c != null).ToList();
            if (customers == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Received GET request for /api/customers");
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

        // GET: api/customers/nextId
        [HttpGet("nextId")]
        public ActionResult<int> GetNextId()
        {
            var maxId = _dbContext.Customers.Max(c => c.Id);
            var nextId = maxId + 1;
            return nextId;
        }

    }
}
