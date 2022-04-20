using BusineesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserSwipeAssignment.Controllers
{
    public class DIDemoController : ApiController
    {
        IEmployee _emp = null;
        public DIDemoController(IEmployee e)
        {
            _emp = e;
        }

        [Route("api/di/getusercount")]
        [HttpGet]
        public HttpResponseMessage GetUserCount()
        {

            //test comment

            return Request.CreateResponse(HttpStatusCode.OK, "Count: "+ _emp.GetCount());
        }
    }
}
