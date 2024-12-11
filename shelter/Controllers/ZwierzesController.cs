using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using shelter.Data;
using shelter.Models;

namespace shelter.Controllers
{
    public class ZwierzesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZwierzesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zwierzes
        public async Task<IActionResult> Index()
        {
              return _context.Zwierze != null ? 
                          View(await _context.Zwierze.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Zwierze'  is null.");
        }

        // GET: Zwierzes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zwierze == null)
            {
                return NotFound();
            }

            var zwierze = await _context.Zwierze
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zwierze == null)
            {
                return NotFound();
            }

            return View(zwierze);
        }

        // GET: Zwierzes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zwierzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Gatunek,Rasa,Wiek,Waga")] Zwierze zwierze)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zwierze);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zwierze);
        }

        // GET: Zwierzes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zwierze == null)
            {
                return NotFound();
            }

            var zwierze = await _context.Zwierze.FindAsync(id);
            if (zwierze == null)
            {
                return NotFound();
            }
            return View(zwierze);
        }

        // POST: Zwierzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Gatunek,Rasa,Wiek,Waga")] Zwierze zwierze)
        {
            if (id != zwierze.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zwierze);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZwierzeExists(zwierze.Id))
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
            return View(zwierze);
        }

        // GET: Zwierzes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zwierze == null)
            {
                return NotFound();
            }

            var zwierze = await _context.Zwierze
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zwierze == null)
            {
                return NotFound();
            }

            return View(zwierze);
        }

        // POST: Zwierzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zwierze == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zwierze'  is null.");
            }
            var zwierze = await _context.Zwierze.FindAsync(id);
            if (zwierze != null)
            {
                _context.Zwierze.Remove(zwierze);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZwierzeExists(int id)
        {
          return (_context.Zwierze?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
