using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeShop1.Data;

namespace CoffeeShop1.Controllers
{
    public class DrinkAvailabilitiesController : Controller
    {
        private readonly CoffeeShopContext _context;

        public DrinkAvailabilitiesController(CoffeeShopContext context)
        {
            _context = context;
        }

        // GET: DrinkAvailabilities
        public async Task<IActionResult> Index()
        {
            var coffeeShopContext = _context.DrinkAvailabilities.Include(d => d.IdcoffeeShopNavigation).Include(d => d.IddrinksNavigation);
            return View(await coffeeShopContext.ToListAsync());
        }

        // GET: DrinkAvailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkAvailability = await _context.DrinkAvailabilities
                .Include(d => d.IdcoffeeShopNavigation)
                .Include(d => d.IddrinksNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkAvailability == null)
            {
                return NotFound();
            }

            return View(drinkAvailability);
        }

        // GET: DrinkAvailabilities/Create
        public IActionResult Create()
        {
            ViewData["IdcoffeeShop"] = new SelectList(_context.Shops, "Id", "Name");
            ViewData["Iddrinks"] = new SelectList(_context.Drinks, "Id", "Name");
            return View();
        }

        // POST: DrinkAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdcoffeeShop,Iddrinks")] DrinkAvailability drinkAvailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkAvailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcoffeeShop"] = new SelectList(_context.Shops, "Id", "Id", drinkAvailability.IdcoffeeShop);
            ViewData["Iddrinks"] = new SelectList(_context.Drinks, "Id", "Id", drinkAvailability.Iddrinks);
            return View(drinkAvailability);
        }

        // GET: DrinkAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkAvailability = await _context.DrinkAvailabilities.FindAsync(id);
            if (drinkAvailability == null)
            {
                return NotFound();
            }
            ViewData["IdcoffeeShop"] = new SelectList(_context.Shops, "Id", "Id", drinkAvailability.IdcoffeeShop);
            ViewData["Iddrinks"] = new SelectList(_context.Drinks, "Id", "Id", drinkAvailability.Iddrinks);
            return View(drinkAvailability);
        }

        // POST: DrinkAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdcoffeeShop,Iddrinks")] DrinkAvailability drinkAvailability)
        {
            if (id != drinkAvailability.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkAvailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkAvailabilityExists(drinkAvailability.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcoffeeShop"] = new SelectList(_context.Shops, "Id", "Id", drinkAvailability.IdcoffeeShop);
            ViewData["Iddrinks"] = new SelectList(_context.Drinks, "Id", "Id", drinkAvailability.Iddrinks);
            return View(drinkAvailability);
        }

        // GET: DrinkAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkAvailability = await _context.DrinkAvailabilities
                .Include(d => d.IdcoffeeShopNavigation)
                .Include(d => d.IddrinksNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkAvailability == null)
            {
                return NotFound();
            }

            return View(drinkAvailability);
        }

        // POST: DrinkAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinkAvailability = await _context.DrinkAvailabilities.FindAsync(id);
            if (drinkAvailability != null)
            {
                _context.DrinkAvailabilities.Remove(drinkAvailability);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkAvailabilityExists(int id)
        {
            return _context.DrinkAvailabilities.Any(e => e.Id == id);
        }
    }
}
