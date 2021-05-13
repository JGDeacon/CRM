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
    public class HistoryService
    {
        public HistoryService()
        {

        }
        //Adds are dont in the individual services.
        //public bool AddHistory(CreateHistory model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        ctx.History.Add(new History
        //        {
        //            CompanyID = model.CompanyID,
        //            UserID = model.UserID,
        //            Table = model.Table,
        //            stringID = model.stringID,
        //            Request = model.Request,
        //            CreatedDateUTC = DateTimeOffset.UtcNow
        //        });
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        public IEnumerable<ReadHistory> GetHistory(int CompanyID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.History.Where(e => e.CompanyID == CompanyID).Select(f => new ReadHistory
                {
                    HistoryID = f.HistoryID,
                    CompanyName = ctx.Companies.FirstOrDefault(g => g.CompanyID == f.CompanyID).CompanyName,
                    Username = ctx.Users.FirstOrDefault(h => h.Id.ToString() == f.UserID.ToString()).UserName,
                    Table = f.Table,
                    stringID = f.stringID,
                    Request = f.Request,
                    CreatedDateUTC = f.CreatedDateUTC
                });
                return query.ToList();
            }
        }
    }
}
