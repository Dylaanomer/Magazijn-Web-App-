using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{


    //MODEL VOOR MAGAZIJN + MAGAZIJN BEHEER


    public class Magazijn
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Instructor")]
        public int ParentMloId { get; set; }

        public int LokatieType { get; set; }

        [StringLength(50)]
        public string Naam { get; set; }

        public int Sequence { get; set; }

        public int Indicator { get; set; }

        public bool Bezet { get; set; }

        public int MaxAanFust { get; set; }

        [InverseProperty("Magazijn")]
        public List<MagazijnPartij> PartijSeriesMagazijn { get; set; }

    }
}
