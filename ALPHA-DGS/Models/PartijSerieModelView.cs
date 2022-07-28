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

    //PARTIJSERIES MODEL VIEW VOOR INDEX OF ZOEK FUNCTIES (INDEXMORE IN PARTIJSERIES CONTROLLER)

    public class PartijSeriesModelView
    {
        public List<Partijserie> Partijseries { get; set; }
        public SelectList Landen { get; set; }
        public string LandGenre { get; set; }
        public string SearchString { get; set; }

        public string SearchString2 { get; set; }

        public string SearchString3 { get; set; }

        public string SearchString4 { get; set; }
    }
}