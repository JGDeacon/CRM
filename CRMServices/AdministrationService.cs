using CRMData;
using CRMModels.Create;
using CRMModels.Read;
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
        private readonly int _companyID;
        static HistoryService historyService = new HistoryService();

        public AdministrationService(Guid userID)
        {
            _userID = userID;
            _companyID = ctx.Users.Single(e => e.Id.ToString() == userID.ToString()).CompanyID;
        }
        //Departments Section
        public IEnumerable<ReadDepartments> GetDepartments()
        {
            var query = ctx.Departments.Where(e => e.CompanyID == _companyID).Select(g => new ReadDepartments
            {
                DepartmentID = g.DepartmentID,
                CompanyID = g.CompanyID,
                DepartmentName = g.DepartmentName,
                CreatedDateUTC = g.CreatedDateUTC,
                ModifiedDateUTC = g.ModifiedDateUTC
            });
            return query.ToList();
        }
        public bool AddDepartment(CreateDepartment model)
        {
            ctx.Departments.Add(new Departments
            {
                CompanyID = _companyID,
                DepartmentName = model.DepartmentName,
                CreatedDateUTC = DateTimeOffset.UtcNow
            }) ;
            return ctx.SaveChanges() == 1;
        }
        public bool EditDepartment(int id, CreateDepartment model)
        {
            Departments departments = ctx.Departments.Find(id);
            if (departments == null)
            {
                return false;
            }
            departments.DepartmentName = model.DepartmentName;
            departments.ModifiedDateUTC = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 1;
        }
        public bool DeleteDepartment(int id)
        {
            Departments department = ctx.Departments.Find(id);
            if (department == null)
            {
                return false;
            }
            ctx.Departments.Remove(department);
            return ctx.SaveChanges() == 1;
        }
        //Permissions Section
        //Pairs user to department in a company and assigns access.

    }
}
