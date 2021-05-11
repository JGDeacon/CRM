using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadHistory
    {
        public Guid HistoryID { get; set; }
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string Table { get; set; }
        public Guid? GuidID { get; set; }
        public int? IntID { get; set; }
        public string Change { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
    }
}
