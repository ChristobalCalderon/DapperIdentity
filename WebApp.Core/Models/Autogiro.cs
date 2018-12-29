using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Models
{
    public class Autogiro
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdentityNumber { get; set; }
        public string Bank { get; set; }
        public int ClearingNumber { get; set; }
        public int AccountNunmber { get; set; }
        public string Debtor { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Signature { get; set; }
        public int PostalCode { get; set; }
        public string Street { get; set; }
        public string PostalAddress { get; set; }
    }
}