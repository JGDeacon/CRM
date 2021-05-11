using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(Workflows))]
        public int WorkflowID { get; set; }
        public virtual Workflows Workflows { get; set; }
        [ForeignKey(nameof(Templates))]
        public int TemplateID { get; set; }
        public virtual Templates Templates { get; set; }
        public int ContactMethodID { get; set; }
        public string TriggerLogic { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
