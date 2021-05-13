using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadDepartments
    {
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Created Date (UTC)")]
        public DateTimeOffset CreatedDateUTC { get; set; }
        [Display(Name = "Modified Date (UTC)")]
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
