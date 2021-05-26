using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Edit
{
    public class EditContact
    {
        public int ContactID { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        [Display(Name = "Preferred Name")]
        public string PreferredName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string Email { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
    }
}
