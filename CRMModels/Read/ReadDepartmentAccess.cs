using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadDepartmentAccess
    {
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public int PermissionID { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
