using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateWorkflowAdmin
    {
        //ApprovedBy, ApprovedDate, WorkflowID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        public string WorkflowName { get; set; }    
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
    }
}
