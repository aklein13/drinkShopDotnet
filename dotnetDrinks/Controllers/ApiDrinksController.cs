using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drinks.Models;
using dotnetDrinks.Data;
using Microsoft.AspNetCore.Authorization;

namespace dotnetDrinks.Controllers
{
    [Produces("application/json")]
    [Route("api/drinks")]
    public class ApiDrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiDrinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/drinks
        [HttpGet]
        public IEnumerable<Drink> GetDrink()
        {
            return _context.Drink;
        }

        // GET: api/drinks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrink([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drink = await _context.Drink.SingleOrDefaultAsync(m => m.Id == id);

            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        // PUT: api/drinks/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> PutDrink([FromRoute] int id, [FromBody] Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drink.Id)
            {
                return BadRequest();
            }

            _context.Entry(drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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

        // POST: api/drinks
        [HttpPost]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> PostDrink([FromBody] Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Drink.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/drinks/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> DeleteDrink([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drink = await _context.Drink.SingleOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drink.Remove(drink);
            await _context.SaveChangesAsync();

            return Ok(drink);
        }

        private bool DrinkExists(int id)
        {
            return _context.Drink.Any(e => e.Id == id);
        }
    }
}