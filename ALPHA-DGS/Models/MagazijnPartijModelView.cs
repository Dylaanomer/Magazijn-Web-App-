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
    public class MagazijnPartijModelView
    {
        public List<MagazijnPartij> MagazijnPartijSeries { get; set; }

        public List<Partijserie> Partijseriesview { get; set; }
       
        public List<Magazijn> Magazijnsview { get; set; }
        
        public SelectList Landen { get; set; }
        
        public string LandGenre { get; set; }
        
        public string SearchString { get; set; }

        public string SearchString2 { get; set; }

        public string SearchString3 { get; set; }

        public string SearchString4 { get; set; }

        public string SearchString5 { get; set; }

        public string SearchString6 { get; set; }

        public int? SearchString7 { get; set; }

        public int? SearchString8 { get; set; }

        public int? SearchString9 { get; set; }
    }
}   