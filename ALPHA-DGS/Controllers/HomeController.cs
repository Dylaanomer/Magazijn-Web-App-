using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using ALPHA_DGS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ALPHA_DGS.Data;

namespace ALPHA_DGS.Controllers
{
    public class HomeController : Controller
    {



        private readonly AlphaDbContext _context;

        // Home Pagina
        public IActionResult Index()
        {
            return View();
        }


        // Index Pop Up Jquery (DEFUNCT)
        [HttpGet]
        public ViewResult IndexPop()
        {
            // Result needs to be IQueryable in database scenarios, to make use of database side paging.
            return View(_context.Set<MagazijnPartij>());
        }


        // Virtual Keyboard Test Pagina
        public IActionResult Index2()
        {
            return View();
        }

        // Test Pagina (DEFUNCT)
        public IActionResult TestPage()
        {

            ViewBag.MagazijnId = new SelectList(_context.Magazijn, "Id", "Naam");

            return View();
        }



        // Home Pagina Uitgebreid (ERROR MET MODEL PARTIJSERIE) (WERKT NIET)
        public IActionResult IndexMore()
        {
            ViewBag.Message = "Welcome to my demo!";
            dynamic mymodel = new ExpandoObject();
            mymodel.PartijSeries = GetTeachers();
            mymodel.MagazijnPartij = GetStudents();
            return View(mymodel);

        }


        // Test Local Array
        private List<Partijserie> GetTeachers()
        {
            List<Partijserie> partijseries = new List<Partijserie>();
            partijseries.Add(new Partijserie { PserId = 1, PsJaarletter = "22", PsHerk = "Duitsland" });
            partijseries.Add(new Partijserie { PserId = 3, PsJaarletter = "24", PsHerk = "Dietsland" });
            partijseries.Add(new Partijserie { PserId = 5, PsJaarletter = "26", PsHerk = "Dutchland" });
            return partijseries;
        }


        // Test Local Array
        private List<MagazijnPartij> GetStudents()
        {
            List<MagazijnPartij> magazijnPartijs = new List<MagazijnPartij>();
            magazijnPartijs.Add(new MagazijnPartij { Id = 1, VpNaam = "21", PHerk = "Elbe" });
            magazijnPartijs.Add(new MagazijnPartij { Id = 2, VpNaam = "22", PHerk = "Angars" });
            magazijnPartijs.Add(new MagazijnPartij { Id = 3, VpNaam = "23", PHerk = "Leon" });
            return magazijnPartijs;
        }


        // Geen Toegang Pagina
        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
