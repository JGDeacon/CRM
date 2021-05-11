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
        [Key,Column(Order =0)]
        [ForeignKey(nameof(Contact))]
        public Guid ContactID { get; set; }
        public virtual Contact Contact { get; set; }

        [Key,Column(Order =1)]
        [ForeignKey(nameof(ApplicationUser))]
        public Guid EndUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
