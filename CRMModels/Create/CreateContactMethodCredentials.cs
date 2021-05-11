using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateContactMethodCredentials
    {
        //ContactMethodID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        [Required]
        public int ContactMethodID { get; set; }
        [Required]
        public Guid CompanyID { get; set; }
        public Guid? UserID { get; set; }
        public string ConnectionString { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
    }
}
