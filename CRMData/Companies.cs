using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Companies
    {
        [Key]
        public Guid CompanyID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string LogoURL { get; set; }
        [Required,MaxLength(50,ErrorMessage ="Too Long")]
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
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }

    }
}
