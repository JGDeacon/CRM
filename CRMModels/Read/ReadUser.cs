using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadUser
    {
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Display(Name = "Company ID")]
        public int CompanyID { get; set; }
        
        [Display(Name = "Role ID")]
        public string RoleID { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Department Name")]
        public string Department { get; set; }

        [Display(Name = "Department ID")]
        public int DepartmentID { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Locked Out")]
        public bool IsLocked { get; set; }

        [Display(Name = "Created Date UTC")]
        public DateTimeOffset CreatedDateUTC { get; set; }
    }
}
