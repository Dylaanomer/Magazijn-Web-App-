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
    public class StadiaController : Controller
    {
        private readonly AlphaDbContext _context;

        public StadiaController(AlphaDbContext context)
        {
            _context = context;
        }

        // Basis Scherm voor Stadia (Additions in de NAVBAR balk)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stadium.ToListAsync());
        }

        // INFO scherm voor Stadia (DEtails knop in Index)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }


        // INVOER scherm voor Stadia (Create New knop in Index) (GET)
        // GET: Stadia/Create
        public IActionResult Create()
        {
            return View();
        }



        // INVOER scherm voor Stadia (Create New knop in Index) (POST)
        // POST: Stadia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StadIum,Omschrijving")] Stadium stadia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stadia);
        }


        // UPDATE scherm voor Stadia (Edit knop in Index) (GET)
        // GET: Stadia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            return View(stadium);
        }



        // UPDATE scherm voor Stadia (Edit knop in Index) (POST)
        // POST: Stadia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StadIum,Omschrijving")] Stadium stadia)
        {
            if (id != stadia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadiumExists(stadia.Id))
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
            return View(stadia);
        }



        // VERWIJDER scherm voor Stadia (Delete knop in Index)
        // GET: Stadia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }



        // VERWIJDER Confirmatie scherm voor Stadia (Delete knop in Index) (Delete)
        // POST: Stadia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await _context.Stadium.FindAsync(id);
            _context.Stadium.Remove(stadium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadium.Any(e => e.Id == id);
        }
    }
}
