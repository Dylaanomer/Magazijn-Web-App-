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
    public class IndicatorViewModel
    {
        public IEnumerable<Magazijn> Magazijns { get; set; }
        public IEnumerable<MagazijnPartij> Partijen { get; set; }
        public int SeriesCount { get; set; }



    }
}