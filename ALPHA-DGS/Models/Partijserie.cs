using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{

    // MODEL VOOR PARTIJSERIES TABEL (CONTROL TABEL IVM MET MAGAZIJNPARTIJ)

    //PARTIJ SERIES ZIJN DE DINGEN DIE INGEVOERD MOETEN WORDEN BINNEN 1 MAGAZIJN OF LOCATIE
    
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
