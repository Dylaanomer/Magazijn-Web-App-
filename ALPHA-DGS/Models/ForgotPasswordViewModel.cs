using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{
    public class ForgotPasswordViewModel
    {


        //DEFUNCT VANWEGEN SSO

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
