using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateContactList
    {
        //ContactListID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        [Required]
        public int ContactID { get; set; }
        [Required]
        public string EndUserID { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
