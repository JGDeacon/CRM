using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class DepartmentAccess
    {
        [Key]
        public Guid DepartmentID { get; set; }
        [Key]
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }
        public int PermissionID { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
