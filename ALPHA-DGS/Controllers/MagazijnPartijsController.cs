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
    public class MagazijnPartijsController : Controller
    {



        // Oude Validatie Method (DEFUNCT)

        IList<Partijserie> studentList = new List<Partijserie>() {
                    new Partijserie(){ PserId=5000, PsHerk="GER", PsTot = "5000" },
                    new Partijserie(){ PserId=2, PsHerk="FRA", PsTot = "6000" },
                    new Partijserie(){ PserId=3, PsHerk="NL", PsTot = "7000" },
                    new Partijserie(){ PserId=4, PsHerk="HOL", PsTot = "8000" },
                    new Partijserie(){ PserId=5, PsHerk="UKR", PsTot = "9000" },
                    new Partijserie(){ PserId=6, PsHerk="RUS", PsTot = "10000" }
                };


        private readonly AlphaDbContext _context;

        public MagazijnPartijsController(AlphaDbContext context)
        {
            _context = context;
        }

        // JS versie van de normaale Index Scherm

        [HttpGet]
        public ViewResult IndexPop()
        {
            // Result needs to be IQueryable in database scenarios, to make use of database side paging.
            return View(_context.Set<MagazijnPartij>());
        }


        
        // Home Scherm van MagazijnPartij (LOTS IN STORAGE OP DE NAV BAR) (PAGINA DAT DE HEFTRUCKER GEBRUIKT)
        public async Task<IActionResult> IndexHEFTRUCK(int? id, string landGenre, string searchString, string searchString2, string searchString3, string searchString4, string searchString5, string searchString6, int? searchString7, int? searchString8,int? searchString9)
        {
            var alphaDbContext = _context.MagazijnPartij.Include(m => m.Magazijn).Include(m => m.Stadium);

            // Use LINQ to get list of genres.

            IQueryable<string> genreQuery = from m in _context.MagazijnPartij
                                            orderby m.PHerk
                                            select m.PHerk;

            var magazijnpartij = from m in _context.MagazijnPartij
                                 select m;

            var magazijn = from m in _context.Magazijn
                                 select m;

            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot");


            if (!string.IsNullOrEmpty(searchString))
            {

                magazijnpartij = magazijnpartij.Where(s => s.Pvan.ToString().Contains(searchString));

            }
            if (!string.IsNullOrEmpty(searchString2))
            {
                magazijnpartij = magazijnpartij.Where(s => s.Ptot.ToString().Contains(searchString2));
            }

            if (!string.IsNullOrEmpty(searchString3))
            {
                magazijnpartij = magazijnpartij.Where(s => s.PHerk.Contains(searchString3));
            }

            if (!string.IsNullOrEmpty(searchString4))
            {
                magazijnpartij = magazijnpartij.Where(s => s.MagazijnId.ToString().Contains(searchString4));
            }


            if (!string.IsNullOrEmpty(searchString5))
            {
                magazijnpartij = magazijnpartij.Where(s => s.VpNaam.Contains(searchString5));
            }

            if (!string.IsNullOrEmpty(searchString6))
            {
                magazijnpartij = magazijnpartij.Where(s => s.StadiumId.ToString().Contains(searchString6));
            }

            var Pvan = searchString7 ?? int.MinValue;
            var Ptot = searchString8 ?? int.MaxValue;
            magazijnpartij = magazijnpartij.Where(s => (s.Pvan >= Pvan && s.Pvan <= Ptot)
            || (s.Ptot >= Pvan && s.Ptot <= Ptot) || (s.Pvan <= Pvan && s.Ptot >= Ptot));


            if (!string.IsNullOrEmpty(landGenre))
            {
                magazijnpartij = magazijnpartij.Where(s => s.PHerk == landGenre);
            }

            var magazijnPartijsVM = new MagazijnPartijModelView
            {
                Landen = new SelectList(await genreQuery.Distinct().ToListAsync()),
                MagazijnPartijSeries = await magazijnpartij.ToListAsync(),
                Magazijnsview = await magazijn.ToListAsync()
            };


            return View(magazijnPartijsVM);
            
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








        // Oude Home Scherm van MagazijnPartij (VERVANGD DOOR IndexHEFTRUCK) (DEFUNCT)


        public async Task<IActionResult> Index(int? id ,string landGenre, string searchString, string searchString2, string searchString3, string searchString4, string searchString5, string searchString6, int searchString7)
        {
            var alphaDbContext = _context.MagazijnPartij.Include(m => m.Magazijn).Include(m => m.Stadium);

            // Use LINQ to get list of genres.

            IQueryable<string> genreQuery = from m in _context.Partijserie
                                            orderby m.PsHerk
                                            select m.PsHerk;

            var magazijnpartij = from m in _context.MagazijnPartij
                                 select m;
          

            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot");


            if (!string.IsNullOrEmpty(searchString))
            {

                magazijnpartij = magazijnpartij.Where(s => s.Pvan.ToString().Contains(searchString));


            }
            if (!string.IsNullOrEmpty(searchString2))   
            {
                magazijnpartij = magazijnpartij.Where(s => s.Ptot.ToString().Contains(searchString2));
            }

            if (!string.IsNullOrEmpty(searchString3))
            {
                magazijnpartij = magazijnpartij.Where(s => s.PHerk.ToString().Contains(searchString3));
            }

            if (!string.IsNullOrEmpty(searchString4))
            {
                magazijnpartij = magazijnpartij.Where(s => s.MagazijnId.ToString().Contains(searchString4));
            }


            if (!string.IsNullOrEmpty(searchString5))
            {
                magazijnpartij = magazijnpartij.Where(s => s.VpNaam.Contains(searchString5));
            }

            if (!string.IsNullOrEmpty(searchString6))
            {
                magazijnpartij = magazijnpartij.Where(s => s.StadiumId.ToString().Contains(searchString6));
            }

            if (searchString7 > 0)
            {
                magazijnpartij = magazijnpartij.Where(s => s.Pvan >= searchString7 && s.Ptot <= searchString7);
            }

            var magazijnPartijsVM = new MagazijnPartijModelView
            {
                MagazijnPartijSeries = await magazijnpartij.ToListAsync()
            };


            return View(magazijnPartijsVM);


            var magazijnPartij = await _context.MagazijnPartij.FindAsync(id);
            if (magazijnPartij == null)
            {
                return NotFound();
            }




            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id", magazijnPartij.MagazijnId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", magazijnPartij.StadiumId);
            return View(magazijnPartij);
        }




        // SPA VIEW VAN DE APP


        // Single Page App Versie van Index  (TEST OMGEVING)
        public async Task<IActionResult> IndexGebruiker(int? id)
        {



            ViewBag.MagazijnId = new SelectList(_context.Magazijn, "Id", "Naam");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot");



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

            var generalseriesVM = new GeneralModelView
            {
                Lokaties = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Partijseries = await partijserie.ToListAsync(),
                Magazijns = await magazijn.ToListAsync(),
                MagazijnPartijs = await magazijnpartij.ToListAsync()
            };


            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot"); 


            return View(generalseriesVM);

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
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan", magazijnPartij.PartijSeriePsvan);
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot", magazijnPartij.PartijSeriePstot);
            return View(magazijnPartij);
        }


        // POST

        // POST VOOR IndexGebruiker 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexGebruiker(int id, [Bind("Id,Pvan,Ptot,PHerk,VpNaam,StadiumId,Uitserie,AantFust,Naam,MagazijnId,PsVan,PsTot")] MagazijnPartij magazijnPartij)
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
                return RedirectToAction(nameof(IndexGebruiker));
            }
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id", magazijnPartij.MagazijnId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", magazijnPartij.StadiumId);
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan", magazijnPartij.PartijSeriePsvan);
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot", magazijnPartij.PartijSeriePstot);
            return View(magazijnPartij);
        }

        


        // GET: MagazijnPartijs/Create (INVOER)

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot");
            MagazijnPartij emp = new MagazijnPartij();
            return PartialView("_MagazijnPartijModelPartial", emp);
            ViewBag.TotalStudents = studentList.Count();
        }


        // POST: MagazijnPartijs/Create (INVOER MET VALIDATIE)
        [HttpPost]
        public IActionResult Create(MagazijnPartij emp)
        {
            
            _context.MagazijnPartij.Add(emp);
            _context.SaveChanges();
            return RedirectToAction(nameof(IndexHEFTRUCK));
            return PartialView("_MagazijnPartijModelPartial", emp);
        }



        // GET: MagazijnPartijs/Create (INVOER ZONDER VALIDATIE)
        [HttpGet]
        public IActionResult CreateFree()
        {
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            ViewData["PartijSeriePsvan"] = new SelectList(_context.Partijserie, "PsVan", "PsVan");
            ViewData["PartijSeriePstot"] = new SelectList(_context.Partijserie, "PsTot", "PsTot");
            MagazijnPartij emp = new MagazijnPartij();
            return PartialView("_MagazijnPartijModelPartialVrijeInvoer", emp);
            ViewBag.TotalStudents = studentList.Count();
        }


        // POST: MagazijnPartijs/Create (INVOER ZONDER VALIDATIE)
        [HttpPost]
        public IActionResult CreateFree(MagazijnPartij emp)
        {

            _context.MagazijnPartij.Add(emp);
            _context.SaveChanges();
            return RedirectToAction(nameof(IndexHEFTRUCK));
            return PartialView("_MagazijnPartijModelPartialVrijeInvoer", emp);
        }

        // POST: MagazijnPartijs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: MagazijnPartijs/Edit/5

        // BEWERK/UPDATE INGEVOERDE ITEMS (GET) 
        public IActionResult Edit(int id)
        {
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            var magazijnPartij = _context.MagazijnPartij
                .Where(magazijnPartij => magazijnPartij.Id == id)
                .Select(magazijnPartij => magazijnPartij).Single();
            return PartialView("_EditMagazijnPartijModelPartial", magazijnPartij);

        }


        // BEWERK/UPDATE INGEVOERDE ITEMS (POST) 
        // POST: MagazijnPartijs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(MagazijnPartij emp)
        {

            _context.MagazijnPartij.Update(emp);
            _context.SaveChanges();
            return PartialView("_EditMagazijnPartijModelPartial", emp);

        }


        //INFO AANVRAAG VAN INGEVOERDE ITEMS (GET) 
        public IActionResult Details(int id)
        {
            ViewData["MagazijnId"] = new SelectList(_context.Magazijn, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            var magazijnPartij = _context.MagazijnPartij
                .Where(magazijnPartij => magazijnPartij.Id == id)
                .Select(magazijnPartij => magazijnPartij).Single();
            return PartialView("_DetailMagazijnPartijModelPartial", magazijnPartij);

        }


        // INFO INGEVOERDE ITEMS (GET) (DEFUNCT) 
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



        // GET: MagazijnPartijs/Delete/5
        // VERWIJDER INGEVOERDE ITEMS (DELETE) (GET)  
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
        // VERWIJDER INGEVOERDE ITEMS (DELETE) (POST)
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
    }
}