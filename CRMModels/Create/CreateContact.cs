using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateContact
    {
        //ContactID, CreatedDateUTC, and ModifiedDateUTC are set by the Service Layer.
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string FirstName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string LastName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string PreferredName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string Email { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string CellPhone { get; set; }
    }
}
