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
        public int HistoryID { get; set; }
        public int CompanyID { get; set; }
        public string UserID { get; set; }
        public string Table { get; set; }
        public string Method { get; set; }
        public int? IntID { get; set; }
        public string Request { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }        
    }
}
