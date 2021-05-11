using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class WorkflowContactList
    {
        [Key,Column(Order =0)]
        [ForeignKey(nameof(ContactList))]
        public Guid ContactListID { get; set; }
        public virtual ContactList ContactList { get; set; }

        [Key,Column(Order =1)]
        [ForeignKey(nameof(Workflows))]
        public Guid WorkflowID { get; set; }
        public virtual Workflows Workflows { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
