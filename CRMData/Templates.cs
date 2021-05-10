using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Templates
    {
        [Key]
        public Guid TemplateID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid? UserID { get; set; }
        [Required]
        public int ContactMethodID { get; set; }
        [Required]
        public string Content { get; set; }
        public Guid? PreviewLinkGuid { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTimeOffset ApprovedDateUTC { get; set; }
        public Guid ApprovedBy { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }

    }
}
