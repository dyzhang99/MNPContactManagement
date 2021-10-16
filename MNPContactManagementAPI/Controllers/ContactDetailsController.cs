using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNPContactManagementAPI.Models;

namespace MNPContactManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {
        private readonly MNPContactManagementContext _context;

        public ContactDetailsController(MNPContactManagementContext context)
        {
            _context = context;
        }

        // GET: api/ContactDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDetail>>> GetContactDetail()
        {
            return await _context.ContactDetail.ToListAsync();
        }

        // GET: api/ContactDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetail>> GetContactDetail(int id)
        {
            var contactDetail = await _context.ContactDetail.FindAsync(id);

            if (contactDetail == null)
            {
                return NotFound();
            }

            return contactDetail;
        }

        // PUT: api/ContactDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactDetail(int id, ContactDetail contactDetail)
        {
            if (id != contactDetail.ContactId)
            {
                return BadRequest();
            }

            _context.Entry(contactDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactDetailExists(id))
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

        // POST: api/ContactDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ContactDetail>> PostContactDetail(ContactDetail contactDetail)
        {
            _context.ContactDetail.Add(contactDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactDetail", new { id = contactDetail.ContactId }, contactDetail);
        }

        // DELETE: api/ContactDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactDetail>> DeleteContactDetail(int id)
        {
            var contactDetail = await _context.ContactDetail.FindAsync(id);
            if (contactDetail == null)
            {
                return NotFound();
            }

            _context.ContactDetail.Remove(contactDetail);
            await _context.SaveChangesAsync();

            return contactDetail;
        }

        private bool ContactDetailExists(int id)
        {
            return _context.ContactDetail.Any(e => e.ContactId == id);
        }
    }
}
