using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALPHA_DGS.Models
{


    //MODEL VOOR GEBRUIKER CLAIMS (OF IE TOEGANG MAG HEBBEN VOOR BEPAALDE CRUD OPERATIES)



    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }
        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}