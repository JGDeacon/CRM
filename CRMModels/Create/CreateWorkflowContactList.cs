using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateWorkflowContactList
    {
        //ContactListID & WorkflowID make up a Composite key
        //CreatedDateUTC and ModifiedDateUTC are set by the Service Layer.
        public int ContactListID { get; set; }
        public Guid WorkflowID { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
