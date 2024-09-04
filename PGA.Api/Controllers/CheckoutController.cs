using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGA.API.Models;
using PGA.Data;

using System.Collections.Generic;
using System.Linq;

namespace PGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Checkout
        [HttpGet]
        public ActionResult<IEnumerable<Checkout>> GetCheckouts()
        {
            return _context.Checkouts.ToList();
        }

        // GET: api/Checkout/5
        [HttpGet("{id}")]
        public ActionResult<Checkout> GetCheckout(int id)
        {
            var checkout = _context.Checkouts.Find(id);

            if (checkout == null)
            {
                return NotFound();
            }

            return checkout;
        }

        // POST: api/Checkout
        [HttpPost]
        public ActionResult<Checkout> PostCheckout(Checkout checkout)
        {
            _context.Checkouts.Add(checkout);
            _context.SaveChanges();

            return CreatedAtAction("GetCheckout", new { id = checkout.Id }, checkout);
        }

        // PUT: api/Checkout/5
        [HttpPut("{id}")]
        public IActionResult PutCheckout(int id, Checkout checkout)
        {
            if (id != checkout.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkout).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Checkout/5
        [HttpDelete("{id}")]
        public ActionResult<Checkout> DeleteCheckout(int id)
        {
            var checkout = _context.Checkouts.Find(id);
            if (checkout == null)
            {
                return NotFound();
            }

            _context.Checkouts.Remove(checkout);
            _context.SaveChanges();

            return checkout;
        }
    }
}
