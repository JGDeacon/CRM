using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadWorkflows
    {
        public Guid WorkflowID { get; set; }
        public string WorkflowName { get; set; }
        public bool IsApproved { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public Guid? ApprovedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
