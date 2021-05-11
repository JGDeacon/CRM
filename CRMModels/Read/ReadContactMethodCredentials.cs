using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    class ReadContactMethodCredentials
    {
        public Guid ID { get; set; }   
        public int ContactMethodID { get; set; }
        public string ContactMethod { get; set; }
        public string CompanyName { get; set; }
        public Guid? UserID { get; set; }
        public string ConnectionString { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
