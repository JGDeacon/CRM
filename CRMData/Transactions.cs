using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }
        public string ContactListID { get; set; }
        public int WorkflowTriggerID { get; set; }
        public int ContactMethodID { get; set; }
        public string Result { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
