using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using UserSwipeAssignment.Models;
using UserSwipeAssignment.Utilities;

namespace UserSwipeAssignment.Controllers
{
    public class UserController : ApiController
    {
        SwipeAssignmentDBEntitiesNew _context = new SwipeAssignmentDBEntitiesNew();

        [Route("api/user/login")]
        [HttpPost]
        public HttpResponseMessage UserLogin(UserModel data)
        {
            Thread.Sleep(9000);

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

        [Route("api/user/Validate")]
        [HttpGet]
        public HttpResponseMessage Validate(string token, string username)
        {
            try
            {
                UserDetail user= _context.UserDetails.FirstOrDefault(x => x.UserName == username);
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid User!!!");

                string tokenUsername = TokenManager.ValidateToken(token);
                if (username.Equals(tokenUsername))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User validated successfully!!!");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Token!!!");

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal Server Error!!!");
            }
        }

    }
}
