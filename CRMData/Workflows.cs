using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Workflows
    {
        [Key]
        public int WorkflowID { get; set; }
        public string WorkflowName { get; set; }
        [DefaultValue(false)]
        public bool IsApproved { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
