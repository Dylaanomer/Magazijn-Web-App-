using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{
    public class IdentificatieInvoer
    {


        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string IdentityInvoer { get; set; }


     

    }
}
