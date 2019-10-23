using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tx_viewer_sign_multiple.Models
{
    // DocumentFlow class to store and handle envelope structures
    public class DocumentFlow
    {
        public string EnvelopeId { get; set; }
        public string Template { get; set; }
        public List<Signer> Signers { get; set; }
        public string Owner { get; set; }
    }

    public class Signer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string SignatureBoxName { get; set; }
        public bool SignatureComplete { get; set; }
    }

    public class SignProcess : DocumentFlow
    {
        public Signer Signer { get; set; }
    }
}