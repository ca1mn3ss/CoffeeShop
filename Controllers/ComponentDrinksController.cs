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
    public class ComponentDrinksController : Controller
    {
        private readonly CoffeeShopContext _context;

        public ComponentDrinksController(CoffeeShopContext context)
        {
            _context = context;
        }

        // GET: ComponentDrinks
        public async Task<IActionResult> Index()
        {
            var coffeeShopContext = _context.ComponentDrinks.Include(c => c.IdcomponentNavigation).Include(c => c.IddrinkNavigation);
            return View(await coffeeShopContext.ToListAsync());
        }

        // GET: ComponentDrinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentDrink = await _context.ComponentDrinks
                .Include(c => c.IdcomponentNavigation)
                .Include(c => c.IddrinkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentDrink == null)
            {
                return NotFound();
            }

            return View(componentDrink);
        }

        // GET: ComponentDrinks/Create
        public IActionResult Create()
        {
            ViewData["Idcomponent"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Name");
            return View();
        }

        // POST: ComponentDrinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idcomponent,Iddrink")] ComponentDrink componentDrink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(componentDrink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcomponent"] = new SelectList(_context.Components, "Id", "Id", componentDrink.Idcomponent);
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Id", componentDrink.Iddrink);
            return View(componentDrink);
        }

        // GET: ComponentDrinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentDrink = await _context.ComponentDrinks.FindAsync(id);
            if (componentDrink == null)
            {
                return NotFound();
            }
            ViewData["Idcomponent"] = new SelectList(_context.Components, "Id", "Id", componentDrink.Idcomponent);
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Id", componentDrink.Iddrink);
            return View(componentDrink);
        }

        // POST: ComponentDrinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idcomponent,Iddrink")] ComponentDrink componentDrink)
        {
            if (id != componentDrink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(componentDrink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentDrinkExists(componentDrink.Id))
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
            ViewData["Idcomponent"] = new SelectList(_context.Components, "Id", "Id", componentDrink.Idcomponent);
            ViewData["Iddrink"] = new SelectList(_context.Drinks, "Id", "Id", componentDrink.Iddrink);
            return View(componentDrink);
        }

        // GET: ComponentDrinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentDrink = await _context.ComponentDrinks
                .Include(c => c.IdcomponentNavigation)
                .Include(c => c.IddrinkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (componentDrink == null)
            {
                return NotFound();
            }

            return View(componentDrink);
        }

        // POST: ComponentDrinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var componentDrink = await _context.ComponentDrinks.FindAsync(id);
            if (componentDrink != null)
            {
                _context.ComponentDrinks.Remove(componentDrink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentDrinkExists(int id)
        {
            return _context.ComponentDrinks.Any(e => e.Id == id);
        }
    }
}
