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
    public class ClientDrinksController : Controller
    {
        private readonly CoffeeShopContext _context;

        public ClientDrinksController(CoffeeShopContext context)
        {
            _context = context;
        }

        // GET: ClientDrinks
        public async Task<IActionResult> Index()
        {
            var coffeeShopContext = _context.ClientDrinks.Include(c => c.IdclientNavigation).Include(c => c.IddrinkNavigation);
            return View(await coffeeShopContext.ToListAsync());
        }

        // GET: ClientDrinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientDrink = await _context.ClientDrinks
                .Include(c => c.IdclientNavigation)
                .Include(c => c.IddrinkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientDrink == null)
            {
                return NotFound();
            }

            return View(clientDrink);
        }

        // GET: ClientDrinks/Create
        public IActionResult Create()
        {
            ViewData["Idclient"] = new SelectList(_context.Clients, "Id", "Fullname");
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Name");
            return View();
        }

        // POST: ClientDrinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idclient,Iddrink")] ClientDrink clientDrink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientDrink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idclient"] = new SelectList(_context.Clients, "Id", "Id", clientDrink.Idclient);
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Id", clientDrink.Iddrink);
            return View(clientDrink);
        }

        // GET: ClientDrinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientDrink = await _context.ClientDrinks.FindAsync(id);
            if (clientDrink == null)
            {
                return NotFound();
            }
            ViewData["Idclient"] = new SelectList(_context.Clients, "Id", "Id", clientDrink.Idclient);
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Id", clientDrink.Iddrink);
            return View(clientDrink);
        }

        // POST: ClientDrinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idclient,Iddrink")] ClientDrink clientDrink)
        {
            if (id != clientDrink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientDrink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientDrinkExists(clientDrink.Id))
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
            ViewData["Idclient"] = new SelectList(_context.Clients, "Id", "Id", clientDrink.Idclient);
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Id", clientDrink.Iddrink);
            return View(clientDrink);
        }

        // GET: ClientDrinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientDrink = await _context.ClientDrinks
                .Include(c => c.IdclientNavigation)
                .Include(c => c.IddrinkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientDrink == null)
            {
                return NotFound();
            }

            return View(clientDrink);
        }

        // POST: ClientDrinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientDrink = await _context.ClientDrinks.FindAsync(id);
            if (clientDrink != null)
            {
                _context.ClientDrinks.Remove(clientDrink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientDrinkExists(int id)
        {
            return _context.ClientDrinks.Any(e => e.Id == id);
        }
    }
}
