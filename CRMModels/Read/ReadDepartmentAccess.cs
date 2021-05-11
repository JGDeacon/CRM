using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadDepartmentAccess
    {
        public Guid DepartmentID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }
        public int PermissionID { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
