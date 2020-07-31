using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tx_viewer_sign_multiple.Models
{
    public class SignDocument
    {
        public string UniqueId { get; set; }
        public string Document { get; set; }

        public string SignerId { get; set; }

    }
}