using System;

namespace ALPHA_DGS.Models
{

    // ERROR VIEW MODEL


    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
