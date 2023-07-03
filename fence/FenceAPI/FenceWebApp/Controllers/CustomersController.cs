using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FenceWebApp.Models;

namespace FenceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly List<Customer> _customers;

        public CustomersController()
        {
            // Initialize some sample data
            _customers = new List<Customer>
            {
                //new Customer { Id = 1, Name = "John Doe" },
                //new Customer { Id = 2, Name = "Jane Smith" }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            // Perform validation and save the customer to the database
            // Add the customer to the _customers list for simplicity in this example

            customer.Id = _customers.Count + 1;
            _customers.Add(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = _customers.Find(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Perform validation and update the customer in the database
            // Update the customer in the _customers list for simplicity in this example

            existingCustomer.firstName = updatedCustomer.firstName;
            existingCustomer.lastName = updatedCustomer.lastName;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _customers.Find(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Delete the customer from the database
            // Delete the customer from the _customers list for simplicity in this example

            _customers.Remove(existingCustomer);

            return NoContent();
        }
    }
}
