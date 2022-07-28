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
    public class PartijseriesController : Controller
    {
        private readonly AlphaDbContext _context;

        public PartijseriesController(AlphaDbContext context)
        {
            _context = context;
        }

        // GET: Partijseries

        // Oude Index Scherm van Partijseries (DEFUNCT MAAR NOG HANDIG)


        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partijserie.ToListAsync());
        }



        // Basis Index Scherm van Partijseries (LOTS SERIES OP NAVBAR)

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> IndexMore(string landGenre, string searchString, string searchString2, string searchString3, string searchString4)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Partijserie
                                            orderby m.PsHerk
                                            select m.PsHerk;

            var partijserie = from m in _context.Partijserie
                              select m;

            if (!string.IsNullOrEmpty(searchString))
            {

                partijserie = partijserie.Where(s => s.PsVan.Contains(searchString));

            }
            if (!string.IsNullOrEmpty(searchString2))
            {
                partijserie = partijserie.Where(s => s.PsTot.Contains(searchString2));
            }

            if (!string.IsNullOrEmpty(searchString3))
            {
                partijserie = partijserie.Where(s => s.PsJaarletter.Contains(searchString3));
            }
            if (!string.IsNullOrEmpty(searchString4))
            {
                partijserie = partijserie.Where(s => s.PsHerk.Contains(searchString4));
            }

            var partijseriesVM = new PartijSeriesModelView
            {
                Landen = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Partijseries = await partijserie.ToListAsync()
            };

            return View(partijseriesVM);
        }




        // Alternatief Index Scherm van Partijseries (SLAAT AAN MET GENERALVIEWMODEL)
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> IndexMore2( string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Partijserie
                orderby m.PsHerk
                select m.PsHerk;

            var partijserie = from m in _context.Partijserie
                select m;

            var magazijn = from m in _context.Magazijn
                              select m;

            var magazijnpartij = from m in _context.MagazijnPartij
                              select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                partijserie = partijserie.Where(s => s.PsJaarletter.Contains(searchString));
            }


            var generalseriesVM = new GeneralModelView
            {
                Lokaties = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Partijseries = await partijserie.ToListAsync(),
                Magazijns = await magazijn.ToListAsync(),
                MagazijnPartijs = await magazijnpartij.ToListAsync()
            };

            return View(generalseriesVM);
        }


        // INFO AANVRAAG van Partijseries (DETAILS KNOP)
        [Authorize(Roles = "Admin, SuperAdmin")]
        // GET: Partijseries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partijserie = await _context.Partijserie
                .FirstOrDefaultAsync(m => m.PserId == id);
            if (partijserie == null)
            {
                return NotFound();
            }

            return View(partijserie);
        }


        // INVOER van Partijseries (VOEG TOE KNOP) (GET)
        [Authorize(Roles = "Admin, SuperAdmin")]
        // GET: Partijseries/Create
        public IActionResult Create()
        {

            ViewData["PsVan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            Partijserie emp = new Partijserie();

            return PartialView("_PartijSerieModelPartial", emp);
        }

        // INVOER van Partijseries (VOEG TOE KNOP) (POST)
        // POST: Partijseries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Partijserie emp)
        {
            if (ModelState.IsValid)
            {
                _context.Partijserie.Add(emp);
                _context.SaveChanges();
                
            }
            return RedirectToAction(nameof(IndexMore));
            return PartialView("_PartijSerieModelPartial", emp);
        }



        // UPDATE van Partijseries (EDIT KNOP) (GET)
        [Authorize(Roles = "Admin, SuperAdmin")]
        // GET: Partijseries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partijserie = await _context.Partijserie.FindAsync(id);
            if (partijserie == null)
            {
                return NotFound();
            }
            return View(partijserie);
        }



        // UPDATE van Partijseries (EDIT KNOP) (POST)
        // POST: Partijseries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PserId,PsJaarletter,PsVan,PsTot,PsHerk")] Partijserie partijserie)
        {
            if (id != partijserie.PserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partijserie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartijserieExists(partijserie.PserId))
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
            return View(partijserie);
        }




        // UPDATE van Partijseries (EDIT KNOP) (DELETE) (GET)
        // GET: Partijseries/Delete/5

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partijserie = await _context.Partijserie
                .FirstOrDefaultAsync(m => m.PserId == id);
            if (partijserie == null)
            {
                return NotFound();
            }

            return View(partijserie);
        }



        // UPDATE van Partijseries (EDIT KNOP) (DELETE) (POST)
        // POST: Partijseries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partijserie = await _context.Partijserie.FindAsync(id);
            _context.Partijserie.Remove(partijserie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartijserieExists(int id)
        {
            return _context.Partijserie.Any(e => e.PserId == id);
        }
    }
}
