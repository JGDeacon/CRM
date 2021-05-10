using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class ContactMethods
    {
        [Key]
        public int ContactMethodID { get; set; }
        public string ContactMethodName { get; set; }
        public bool IsActive { get; set; }
    }
}
