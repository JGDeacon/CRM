using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [Required, MinLength(1,ErrorMessage ="Too Short"),MaxLength(50,ErrorMessage = "Too long")]
        public string FirstName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string LastName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string PreferredName { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string Email { get; set; }
        [Required, MinLength(1, ErrorMessage = "Too Short"), MaxLength(50, ErrorMessage = "Too long")]
        public string CellPhone { get; set; }
        public string OwnerID { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
    }
}
