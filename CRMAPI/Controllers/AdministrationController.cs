using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRMAPI.Controllers
{
    
    public class AdministrationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult AddDepartment()
        {
            return Ok("It connected");
        }
    }
}
