using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class ContactMethodCredentials
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(ContactMethods))]
        public int ContactMethodID { get; set; }
        public virtual ContactMethods ContactMethods { get; set; }
        [ForeignKey(nameof(Companies))]
        public int CompanyID { get; set; }
        public virtual Companies Companies { get; set; }
        public int? UserID { get; set; }
        public string ConnectionString { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
