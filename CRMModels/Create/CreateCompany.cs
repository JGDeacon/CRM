using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateCompany
    {
        //CompanyID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        public string CompanyName { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string LogoURL { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string ContactPerson { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string StreetAddress { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string City { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string StateProvince { get; set; }
        [Required, MaxLength(15, ErrorMessage = "Too Long")]
        public string Zip { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string Country { get; set; }
        [Required, MaxLength(25, ErrorMessage = "Too Long")]
        public string Phone { get; set; }
    }
}
