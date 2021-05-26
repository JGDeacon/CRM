using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadWorkflowContactList
    {
        public int ContactListID { get; set; }
        public int WorkflowID { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
