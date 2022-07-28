using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ALPHA_DGS.Models
{


    //VIEWMODEL VOOR MAGAZIJN INDICATOR (HOE VEEL PARTIJEN BINNEN 1 MAGAZIJN OF LOCATIE ZIT)


    [Keyless]
    public class IndicatorViewModel
    {
        public IEnumerable<Magazijn> Magazijns { get; set; }
        public IEnumerable<MagazijnPartij> Partijen { get; set; }
        public int SeriesCount { get; set; }



    }
}