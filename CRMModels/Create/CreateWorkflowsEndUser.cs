using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateWorkflowsEndUser
    {
        //IsActive set to False in the Service Layer.
        //Admin model can set to active and approve.
        //WorkflowID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        public string WorkflowName { get; set; }
    }
}
