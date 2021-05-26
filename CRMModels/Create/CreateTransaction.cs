using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateTransaction
    {
        //This is internal to process a "Send" action in a workflow
        //TransactionID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        public string ContactListID { get; set; }
        public int WorkflowTriggerID { get; set; }
        public int ContactMethodID { get; set; }
        public string Result { get; set; }
    }
}
