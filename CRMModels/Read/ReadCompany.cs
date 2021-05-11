using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadCompany
    {
        public Guid CompanyID { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Logo Location")]
        public string LogoURL { get; set; }
        [Display(Name = "Company Contact")]
        public string ContactPerson { get; set; }
        [Display(Name = "Company Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "Company City")]
        public string City { get; set; }
        [Display(Name = "Company State/Province")]
        public string StateProvince { get; set; }
        [Display(Name = "Company Zip")]
        public string Zip { get; set; }
        [Display(Name = "Company Country")]
        public string Country { get; set; }
        [Display(Name = "Company Phone")]
        public string Phone { get; set; }
        [Display(Name = "Company Added")]
        public DateTimeOffset CreatedDateUTC { get; set; }
        [Display(Name = "Company Last Modifed")]
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
