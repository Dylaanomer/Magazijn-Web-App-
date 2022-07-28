using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ALPHA_DGS.Data;
using ALPHA_DGS.Models;

namespace ALPHA_DGS.Controllers
{


    // OUDE MODEL DIE NIET MEER WORD GEBRUIKT

    public class IdentificatieInvoersController : Controller
    {
        private readonly AlphaDbContext _context;

        public IdentificatieInvoersController(AlphaDbContext context)
        {
            _context = context;
        }

        // GET: IdentificatieInvoers
        public async Task<IActionResult> Index()
        {
            return View(await _context.IDinvoer.ToListAsync());
        }

        // GET: IdentificatieInvoers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identificatieInvoer = await _context.IDinvoer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identificatieInvoer == null)
            {
                return NotFound();
            }

            return View(identificatieInvoer);
        }

        // GET: IdentificatieInvoers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IdentificatieInvoers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdentityInvoer")] IdentificatieInvoer identificatieInvoer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identificatieInvoer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(identificatieInvoer);
        }

        // GET: IdentificatieInvoers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identificatieInvoer = await _context.IDinvoer.FindAsync(id);
            if (identificatieInvoer == null)
            {
                return NotFound();
            }
            return View(identificatieInvoer);
        }

        // POST: IdentificatieInvoers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityInvoer")] IdentificatieInvoer identificatieInvoer)
        {
            if (id != identificatieInvoer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identificatieInvoer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentificatieInvoerExists(identificatieInvoer.Id))
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
            return View(identificatieInvoer);
        }

        // GET: IdentificatieInvoers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identificatieInvoer = await _context.IDinvoer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identificatieInvoer == null)
            {
                return NotFound();
            }

            return View(identificatieInvoer);
        }

        // POST: IdentificatieInvoers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var identificatieInvoer = await _context.IDinvoer.FindAsync(id);
            _context.IDinvoer.Remove(identificatieInvoer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdentificatieInvoerExists(int id)
        {
            return _context.IDinvoer.Any(e => e.Id == id);
        }
    }
}
