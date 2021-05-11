using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateDepartment
    {
        //CompanyID, DepartmentID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string DepartmentName { get; set; }
    }
}
