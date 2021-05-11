﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateTemplateAdmin
    {
        //EndUser Tempalate cannot approve or make active. TempalteID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        //ApprovedBy is set at the Service Layer
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
    }
}
