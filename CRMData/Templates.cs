using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Templates
    {
        [Key]
        public int TemplateID { get; set; }
        [ForeignKey(nameof(Companies))]
        public int CompanyID { get; set; }
        public virtual Companies Companies { get; set; }
        public int? UserID { get; set; }
        [Required]
        public int ContactMethodID { get; set; }
        [Required]
        public string Content { get; set; }
        public int? PreviewLinkint { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTimeOffset ApprovedDateUTC { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string ApprovedBy { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }

    }
}
