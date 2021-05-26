using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateContactMethods
    {
        //ContactMethodID is set by the Service Layer.
        public string ContactMethodName { get; set; }
        public bool IsActive { get; set; }
    }
}
