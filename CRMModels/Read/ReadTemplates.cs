using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadTemplates
    {
        public Guid TemplateID { get; set; }
        public string CompanyName { get; set; }
        public Guid? UserID { get; set; }
        public int ContactMethodID { get; set; }
        public string ContactMethodName { get; set; }
        public string Content { get; set; }
        public Guid? PreviewLinkGuid { get; set; }
        public bool IsPublic { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset ApprovedDateUTC { get; set; }
        public string ApprovedBy { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
