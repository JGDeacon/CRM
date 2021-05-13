using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadHistory
    {
        public int HistoryID { get; set; }
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string Table { get; set; }
        public string Method { get; set; }
        public int? IntID { get; set; }
        public string Request { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
    }
}
