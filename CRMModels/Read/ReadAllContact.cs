using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Read
{
    public class ReadAllContact
    {
        public int ContactID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Preferred Name")]
        public string PreferredName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
        [Display(Name = "Contact Owner")]
        public string Username { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedDateUTC { get; set; }
        [Display(Name = "Last Modifed")]
        public DateTimeOffset? ModifiedDateUTC { get; set; }
        public int PermissionID { get; set; }
    }
}
