using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCode_BE.Models;

namespace TestCode_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BpkbController : Controller
    {
        private readonly DataContext _context;

        public BpkbController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBpkbs()
        {
            return Ok(await _context.Bpkbs.ToListAsync());
        }

        [HttpGet("{agreementNumber}")]
        public async Task<IActionResult> GetBpkb(string agreementNumber)
        {
            var bpkb = await _context.Bpkbs.FindAsync(agreementNumber);

            if (bpkb == null)
            {
                return NotFound();
            }

            return Ok(bpkb);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBpkb([FromBody] Bpkb bpkb)
        {
            if (ModelState.IsValid)
            {
                bpkb.created_on = DateTime.Now;
                bpkb.last_updated_on = DateTime.Now;

                _context.Bpkbs.Add(bpkb);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBpkb), new { agreementNumber = bpkb.agreement_number }, bpkb);
            }

            return BadRequest(ModelState);
        }
        
        [HttpPut("{agreementNumber}")]
        public async Task<IActionResult> UpdateBpkb(string agreementNumber, [FromBody] Bpkb bpkb)
        {
            if (agreementNumber != bpkb.agreement_number)
            {
                return BadRequest("Agreement number mismatch");
            }

            bpkb.last_updated_on = DateTime.Now;
            _context.Entry(bpkb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BpkbExists(agreementNumber))
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

        [HttpDelete("{agreementNumber}")]
        public async Task<IActionResult> DeleteBpkb(string agreementNumber)
        {
            var bpkb = await _context.Bpkbs.FindAsync(agreementNumber);
            if (bpkb == null)
            {
                return NotFound();
            }

            _context.Bpkbs.Remove(bpkb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BpkbExists(string agreementNumber)
        {
            return _context.Bpkbs.Any(e => e.agreement_number == agreementNumber);
        }
    }
}
