using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class WorkflowTriggers
    {
        [Key]
        public int WorkflowTriggerID { get; set; }
        public string WorkflowTriggerName { get; set; }
        public Guid WorkflowID { get; set; }
        public Guid TemplateID { get; set; }
        public int ContactMethodID { get; set; }
        public string TriggerLogic { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
