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
        //DepartmentID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        public Guid CompanyID { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Too Long")]
        public string DepartmentName { get; set; }
    }
}
