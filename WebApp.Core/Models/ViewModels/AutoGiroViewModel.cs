using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Core.Models.ViewModels
{
    public class AutoGiroViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [JsonProperty(PropertyName = "surname")]
        public string LastName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [JsonProperty(PropertyName = "pin")]
        public string PersonalIdentityNumber { get; set; }
        [Required]
        [JsonProperty(PropertyName = "bank")]
        public string Bank { get; set; }
        [Required]
        [JsonProperty(PropertyName = "clearingnumber")]
        public int ClearingNumber { get; set; }
        [Required]
        [JsonProperty(PropertyName = "account")]
        public int AccountNunmber { get; set; }
        [Required]
        [JsonProperty(PropertyName = "customeraccount")]
        public string Debtor { get; set; }
        [Required]
        [JsonProperty(PropertyName = "date")]
        public DateTime TimeStamp { get; set; }
        [Required]
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
        [Required]
        [JsonProperty(PropertyName = "signature")]
        public string Signature { get; set; }
        [Required]
        [JsonProperty(PropertyName = "postalcode")]
        public int PostalCode { get; set; }
        [Required]
        [JsonProperty(PropertyName = "streetaddress")]
        public string Street { get; set; }
        [Required]
        [JsonProperty(PropertyName = "county")]
        public string PostalAddress { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Förnamn: " + FirstName);
            stringBuilder.AppendLine();
            stringBuilder.Append("Efternamn: " + LastName);
            stringBuilder.AppendLine();
            stringBuilder.Append("Personnr: " + PersonalIdentityNumber);
            stringBuilder.AppendLine();
            stringBuilder.Append("Bank: " + Bank);
            stringBuilder.AppendLine();
            stringBuilder.Append("Clearingnr: " + ClearingNumber);
            stringBuilder.AppendLine();
            stringBuilder.Append("Kontonr: " + AccountNunmber);
            stringBuilder.AppendLine();
            stringBuilder.Append("Betalare: " + Debtor);
            stringBuilder.AppendLine();
            stringBuilder.Append("Datum: " + TimeStamp);
            stringBuilder.AppendLine();
            stringBuilder.Append("Ort: " + Location);
            stringBuilder.AppendLine();
            stringBuilder.Append("Postkod: " + PostalCode);
            stringBuilder.AppendLine();
            stringBuilder.Append("Gata: " + Street);
            stringBuilder.AppendLine();
            stringBuilder.Append("Postadress: " + PostalAddress);
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }
    }
}
