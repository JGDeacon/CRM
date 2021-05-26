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
        [Display(Name = "Department Name")]
        public int DepartmentID { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Username")]
        public int UserID { get; set; }
        public string Username { get; set; }
        public int UserNumber { get; set; }
        [Display(Name = "Access")]
        public int PermissionID { get; set; }
        public string Access { get; set; }
    }
}
