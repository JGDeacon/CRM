using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadDepartmentAccess
    {
        public int DepartmentAccessID { get; set; }
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public string UserID { get; set; }
        public int UserNumber { get; set; }
        public int PermissionID { get; set; }
        public string Access { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
