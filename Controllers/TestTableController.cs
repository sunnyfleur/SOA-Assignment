using Microsoft.AspNetCore.Mvc;
using SOA_Assignment.Models;
using System;

namespace SOA_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTableController : ControllerBase
    {
        private readonly YourDbContext _context;

        public TestTableController(YourDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostTestTable(TestTable testTable)
        {
            if (testTable == null)
            {
                return BadRequest();
            }

            _context.TestTable.Add(testTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTestTable), new { id = testTable.Id }, testTable);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestTable(int id)
        {
            var testTable = await _context.TestTable.FindAsync(id);

            if (testTable == null)
            {
                return NotFound();
            }

            return Ok(testTable);
        }
    }
}
