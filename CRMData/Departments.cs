using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Departments
    {
        [Key]
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        [Required,MaxLength(50,ErrorMessage ="Too Long")]
        public string DepartmentName { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
