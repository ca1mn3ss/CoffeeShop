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
    public class BaristumsController : Controller
    {
        private readonly CoffeeShopContext _context;

        public BaristumsController(CoffeeShopContext context)
        {
            _context = context;
        }

        // GET: Baristums
        public async Task<IActionResult> Index()
        {
            var coffeeShopContext = _context.Barista.Include(b => b.IdshopNavigation);
            return View(await coffeeShopContext.ToListAsync());
        }

        // GET: Baristums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baristum = await _context.Barista
                .Include(b => b.IdshopNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baristum == null)
            {
                return NotFound();
            }

            return View(baristum);
        }

        // GET: Baristums/Create
        public IActionResult Create()
        {
            ViewData["Idshop"] = new SelectList(_context.Shops, "Id", "Name");
            return View();
        }

        // POST: Baristums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Seniority,Idshop")] Baristum baristum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baristum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idshop"] = new SelectList(_context.Shops, "Id", "Id", baristum.Idshop);
            return View(baristum);
        }

        // GET: Baristums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baristum = await _context.Barista.FindAsync(id);
            if (baristum == null)
            {
                return NotFound();
            }
            ViewData["Idshop"] = new SelectList(_context.Shops, "Id", "Id", baristum.Idshop);
            return View(baristum);
        }

        // POST: Baristums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Seniority,Idshop")] Baristum baristum)
        {
            if (id != baristum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baristum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaristumExists(baristum.Id))
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
            ViewData["Idshop"] = new SelectList(_context.Shops, "Id", "Id", baristum.Idshop);
            return View(baristum);
        }

        // GET: Baristums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baristum = await _context.Barista
                .Include(b => b.IdshopNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baristum == null)
            {
                return NotFound();
            }

            return View(baristum);
        }

        // POST: Baristums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baristum = await _context.Barista.FindAsync(id);
            if (baristum != null)
            {
                _context.Barista.Remove(baristum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaristumExists(int id)
        {
            return _context.Barista.Any(e => e.Id == id);
        }
    }
}
