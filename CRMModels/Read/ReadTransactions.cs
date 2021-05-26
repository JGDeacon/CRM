using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadTransactions
    {
        public int TransactionID { get; set; }
        public string ContactListID { get; set; }
        public string WorkflowTriggerName { get; set; }
        public string ContactMethodName { get; set; }
        public string Result { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
