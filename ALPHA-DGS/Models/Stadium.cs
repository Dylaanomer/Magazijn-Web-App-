using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{
    public class Stadium
    {
        [Key]
        public int Id { get; set; }

        [StringLength(5)]
        public string StadIum { get; set; }

        [StringLength(50)]
        public string Omschrijving { get; set; }
    }
}
