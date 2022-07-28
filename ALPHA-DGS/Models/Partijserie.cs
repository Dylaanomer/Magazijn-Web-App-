using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{
    public class Partijserie
    {
        [Key]
        public int PserId { get; set; }

        [StringLength(2)]
        public string PsJaarletter { get; set; }

        public string PsVan { get; set; }

        public string PsTot { get; set; }

        [StringLength(50)]
        public string PsHerk { get; set; }
    }
}
