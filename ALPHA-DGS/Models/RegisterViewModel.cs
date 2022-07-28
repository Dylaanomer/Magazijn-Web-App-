using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{

    // REGISTRATIE MODEL VOOR VIEW REGISTRATIRE (REGISTER IN USER CONTROLLER OF LOGIN OF ANDERE CONTROLLER IVM INLOGGEN) (DEFUNCT WSS DOOR SSO PRINCIPE DIE WERKEND IS) (KAN HANDIG ZIJN VOOR MENSEN DIE BUITEN DE SSO VALLEN OF WILLEN INLOGGEN ZONDER SSO PRINCIPE)

    // DEFUNCT (GEBRUIK INDIEN SSO NIET WERKEND IS MEER)


    public class RegisterViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
       
        public string RoleSelected { get; set; }



    }
}
