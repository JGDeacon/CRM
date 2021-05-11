using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class ContactList
    {
        [Key]
        public int ContactListID { get; set; }
        [ForeignKey(nameof(Contact))]
        public int ContactID { get; set; }
        public virtual Contact Contact { get; set; }

        
        [ForeignKey(nameof(ApplicationUser))]
        public string EndUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
