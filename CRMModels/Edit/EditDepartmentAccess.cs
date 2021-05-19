using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Edit
{
    public class EditDepartmentAccess
    {
        public int DepartmentAccessID { get; set; }
        public int DepartmentID { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public int UserNumber { get; set; }
        public int PermissionID { get; set; }
        public string Access { get; set; }
    }
}
