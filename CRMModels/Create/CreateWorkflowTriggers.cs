﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateWorkflowTriggers
    {
        //CreatedBy, WorkflowTriggerID, DateTimeOffset, and DateTimeOffset are assigned at the Service Layer
        public string WorkflowTriggerName { get; set; }   
        public Guid WorkflowID { get; set; }
        public Guid TemplateID { get; set; }
        public int ContactMethodID { get; set; }
        public string TriggerLogic { get; set; }
    }
}
