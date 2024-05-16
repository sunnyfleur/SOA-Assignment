using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOA_Assignment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly YourDbContext _context;

        public CustomersController(YourDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customers customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customer);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while saving the customer: {ex.Message}");
                return StatusCode(500, "An error occurred while saving the customer. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customers customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
