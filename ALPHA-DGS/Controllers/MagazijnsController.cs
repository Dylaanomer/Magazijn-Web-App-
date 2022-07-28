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
    public class MagazijnsController : Controller
    {


        // LOCAL ARRAY LIST (DELETE) (GET)

        IList<Partijserie> studentList = new List<Partijserie>() {
                    new Partijserie(){ PserId=5000, PsHerk="GER", PsTot = "5000" },
                    new Partijserie(){ PserId=2, PsHerk="FRA", PsTot = "6000" },
                    new Partijserie(){ PserId=3, PsHerk="NL", PsTot = "7000" },
                    new Partijserie(){ PserId=4, PsHerk="HOL", PsTot = "8000" },
                    new Partijserie(){ PserId=5, PsHerk="UKR", PsTot = "9000" },
                    new Partijserie(){ PserId=6, PsHerk="RUS", PsTot = "10000" }
                };



        private readonly AlphaDbContext _context;

        public MagazijnsController(AlphaDbContext context)
        {
            _context = context;
        }


        // INDICATOR MAGAZIJN SIZE (HOE VEEL IS INGEVULD IN 1 MAGAZIJN OF LOCATIE
        // HUIDIGE LIMIET VASTGESTELD AAN 42 (VERBINDING MET MAXAANFUST WAARDE MOET NOG)

        public ActionResult IndexMagazijn()
        {
            var viewModel = new IndicatorViewModel
            {
                Magazijns = _context.Magazijn.ToList(),
                Partijen = _context.MagazijnPartij.ToList()
            };
            return View("IndexMagazijn", viewModel);
        }






        // INDEX HOME PAGINA VAN MAGAZIJN (IN DE NAVBAR HEET HET MAGAZIJN)
        // GET: Magazijns

        public async Task<IActionResult> Index(string searchString)
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




        // OUDE TEST PAGINA (DEFUNCT)
        // GET: Magazijns UNIQUE ACTION METHOD


        public async Task<IActionResult> IndexMore(int? id)
        {
            return View(await _context.Magazijn.ToListAsync());
        }





        // INFO KNOP VOOR ITEMS IN DE MAGAZIJN INDEX (DETAILS KNOP)
        // GET: Magazijns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijn = await _context.Magazijn.Include(r => r.PartijSeriesMagazijn)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazijn == null)
            {
                return NotFound();
            }

            return View(magazijn);
        }


        // INFO KNOP VOOR ITEMS IN DE MAGAZIJN INDEX (DETAILS KNOP) (JQUERY POPUP) (DEFUNCT)
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijn = await _context.Magazijn.Include(r => r.PartijSeriesMagazijn)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazijn == null)
            {
                return NotFound();
            }

            return PartialView("_DetailMagazijnModelPartial", magazijn);
        }


        // NIEUWE MAGAZIJN INVOER SCHERM (CREATE) (GET)
        // GET: Magazijns/Create
        public IActionResult Create()
        {
            return View();
        }



        // NIEUWE MAGAZIJN INVOER SCHERM (CREATE) (POST)
        // POST: Magazijns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentMloId,LokatieType,Naam,Sequence,Bezet,MaxAanFust,Indicator")] Magazijn magazijn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magazijn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazijn);
        }


        // UPDATE MAGAZIJN INVOER SCHERM (EDIT) (GET)
        // GET: Magazijns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijn = await _context.Magazijn.FindAsync(id);
            if (magazijn == null)
            {
                return NotFound();
            }
            return View(magazijn);
        }


        // UPDATE MAGAZIJN INVOER SCHERM (EDIT) (POST)
        // POST: Magazijns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentMloId,LokatieType,Naam,Sequence,Bezet,MaxAanFust,Indicator")] Magazijn magazijn)
        {
            if (id != magazijn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazijn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazijnExists(magazijn.Id))
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
            return View(magazijn);
        }



        // VERWIJDER MAGAZIJN INVOER SCHERM (DELETE)
        // GET: Magazijns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazijn = await _context.Magazijn
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazijn == null)
            {
                return NotFound();
            }

            return View(magazijn);
        }


        // VERWIJDER MAGAZIJN INVOER SCHERM  (DELETE) + CONFIRMATIE VAN GEBRUIKER (CONFIRMATIE SCHERM)
        // POST: Magazijns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazijn = await _context.Magazijn.FindAsync(id);
            _context.Magazijn.Remove(magazijn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazijnExists(int id)
        {
            return _context.Magazijn.Any(e => e.Id == id);
        }
    }
}