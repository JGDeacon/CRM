using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Permissions
    {
        [Key]
        public int PermissionID { get; set; }
        public string Access { get; set; }
    }
}
