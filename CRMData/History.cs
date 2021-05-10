using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class History
    {
        [Key]
        public Guid HistoryID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }
        public string Table { get; set; }
        public Guid? GuidID { get; set; }
        public int? IntID { get; set; }
        public string Change { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }        
    }
}
