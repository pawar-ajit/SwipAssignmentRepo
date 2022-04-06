using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserSwipeAssignment.Models;
using UserSwipeAssignment.Utilities;

namespace UserSwipeAssignment.Controllers
{
    public class UserController : ApiController
    {
        SwipeAssignmentDBEntities _context = new SwipeAssignmentDBEntities();

        [Route("api/user/login")]
        [HttpPost]
        public HttpResponseMessage UserLogin(UserModel data)
        {
            UserDetail user = _context.UserDetails.FirstOrDefault(x => x.UserName == data.UserName && x.UserPassword == data.Password);

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User!!!");
            if (user.ActiveStatus == false)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Inactive User!!!");
            else
            {
                string token = TokenManager.GenerateToken(data.UserName);
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
        }

    }
}
