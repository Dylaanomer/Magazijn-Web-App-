using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ALPHA_DGS.Data;
using ALPHA_DGS.Models;
using Microsoft.AspNetCore.Authorization;

namespace ALPHA_DGS.Controllers
{
    // OUDE MODEL DIE NIET MEER WORD GEBRUIKT
    public class Locaties : Controller
    {
        private readonly AlphaDbContext _context;

        
        public async Task<IActionResult> Index()
        {
            var alphaDbContext = _context.MagazijnPartij.Include(m => m.Magazijn).Include(m => m.Stadium);
            return View(await alphaDbContext.ToListAsync());
        }
        // GET: MagazijnPartijs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijnPartij = await _context.MagazijnPartij
                .Include(m => m.Magazijn)
                .Include(m => m.Stadium)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazijnPartij == null)
            {
                return NotFound();
            }

            return View(magazijnPartij);
        }

        // GET: MagazijnPartijs/Create
        public IActionResult Create()
        {
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            return View();
        }

        // POST: MagazijnPartijs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pvan,Ptot,PHerk,MagazijnId,VpNaam,StadiumId,Uitserie,AantFust,Naam")] MagazijnPartij magazijnPartij)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magazijnPartij);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id", magazijnPartij.MagazijnId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", magazijnPartij.StadiumId);
            return View(magazijnPartij);
        }

        // GET: MagazijnPartijs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijnPartij = await _context.MagazijnPartij.FindAsync(id);
            if (magazijnPartij == null)
            {
                return NotFound();
            }
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id", magazijnPartij.MagazijnId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", magazijnPartij.StadiumId);
            return View(magazijnPartij);
        }

        // POST: MagazijnPartijs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pvan,Ptot,PHerk,MagazijnId,VpNaam,StadiumId,Uitserie,AantFust,Naam")] MagazijnPartij magazijnPartij)
        {
            if (id != magazijnPartij.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazijnPartij);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazijnPartijExists(magazijnPartij.Id))
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
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id", magazijnPartij.MagazijnId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", magazijnPartij.StadiumId);
            return View(magazijnPartij);
        }

        // GET: MagazijnPartijs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijnPartij = await _context.MagazijnPartij
                .Include(m => m.Magazijn)
                .Include(m => m.Stadium)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazijnPartij == null)
            {
                return NotFound();
            }

            return View(magazijnPartij);
        }

        // POST: MagazijnPartijs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazijnPartij = await _context.MagazijnPartij.FindAsync(id);
            _context.MagazijnPartij.Remove(magazijnPartij);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazijnPartijExists(int id)
        {
            return _context.MagazijnPartij.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        //Accessible by everyone, even if users are not logged in.
        public IActionResult Locatie1()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie2()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie3()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie4()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie5()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie6()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie7()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie8()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie9()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie10()
        {
            return View();
        }

        [AllowAnonymous]
        //Accessible by logged in users.
        public IActionResult Locatie11()
        {
            return View();
        }
    }
}



  