using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadTemplates
    {
        public int TemplateID { get; set; }
        public string CompanyName { get; set; }
        public int? UserID { get; set; }
        public int ContactMethodID { get; set; }
        public string ContactMethodName { get; set; }
        public string Content { get; set; }
        public int? PreviewLinkint { get; set; }
        public bool IsPublic { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset ApprovedDateUTC { get; set; }
        public string ApprovedBy { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
