using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ALPHA_DGS.Models
{
    [Keyless]
    public class GeneralModelView
    {
        public List<Partijserie> Partijseries { get; set; }
        public List<Magazijn> Magazijns { get; set; }
        public List<MagazijnPartij> MagazijnPartijs { get; set; }
        public SelectList Lokaties { get; set; }

        public string LandGenre { get; set; }

        public string SearchString { get; set; }

        public string SearchString2 { get; set; }

        public string SearchString3 { get; set; }

        public string SearchString4 { get; set; }

        public string SearchString5 { get; set; }

        public string SearchString6 { get; set; }

        public string SearchString7 { get; set; }
        public string PartijHerkomst { get; set; }
        
    }
}