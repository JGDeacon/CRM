using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadContactList
    {
        
        public int ContactListID { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; } //Contact Firstname + Lastname
        [Display(Name = "Username")]
        public string EndUserName { get; set; }
        [Display(Name = "Contact Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Contact Added")]
        public DateTimeOffset CreatedDateUTC { get; set; }
        [Display(Name = "Contact Name")]
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
