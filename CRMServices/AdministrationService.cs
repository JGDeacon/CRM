using CRMData;
using CRMModels.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServices
{
    public class AdministrationService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly Guid _userID;
        static HistoryService historyService = new HistoryService();

        public AdministrationService(Guid userID)
        {
            _userID = userID;
        }

        public bool AddDepartment(CreateDepartment model)
        {
            ctx.Departments.Add(new Departments
            {
                CompanyID = model.CompanyID,
                DepartmentName = model.DepartmentName,
                CreatedDateUTC = DateTimeOffset.UtcNow
            });
            return ctx.SaveChanges() == 1;
        }
        public bool DeleteDepartment(int)
    }
}
