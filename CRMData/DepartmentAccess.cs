using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class DepartmentAccess
    {
        [Key]
        public int DepartmentAccessID { get; set; }
        [ForeignKey(nameof(Departments))]
        public int DepartmentID { get; set; }
        public virtual Departments Departments { get; set; }
        [ForeignKey(nameof(Companies))]
        public int CompanyID { get; set; }
        public virtual Companies Companies { get; set; }

        public string UserID { get; set; }
        public int PermissionID { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
