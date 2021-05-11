using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    class CreateDepartmentAccess
    {
        //DepartmentID and CompanyID are parts of a Composite Key
        public Guid DepartmentID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }
        public int PermissionID { get; set; }
    }
}
