using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Edit
{
    public class EditDepartment
    {
        [Required]
        public int DepartmentID { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long"), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
