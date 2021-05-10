using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class WorkflowContactList
    {
        [Key]
        public Guid ContactListID { get; set; }
        [Key]
        public Guid WorkflowID { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
