using UserSwipeAssignment.UserRepo;
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

        IUserRepository _usersRepository;
        TokenManager _tkmgr;

        public UserController(IUserRepository u, TokenManager t)
        {
            _usersRepository = u;
            _tkmgr = t;
        }

        [Route("api/user/{userId}")]
        [HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            UserDetail user = _usersRepository.GetUserById(userId);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
       
        [Route("api/user/login")]
        [HttpPost]
        public HttpResponseMessage UserLogin(UserModel data)
        {
            //Thread.Sleep(9000);

            UserDetail user = _usersRepository.GetUserByName(data);

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User!!!");
            if (user.ActiveStatus == false)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Inactive User!!!");
            else
            {
                //TokenManager tmgr = new TokenManager();
                string token = _tkmgr.GenerateToken(data.UserName);
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
        }

        [Route("api/user/Validate")]
        [HttpGet]
        public HttpResponseMessage Validate(string token, string username)
        {
            try
            {
                UserDetail user = _usersRepository.GetUserByName(new UserModel { UserName= username});
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid User!!!");

                string tokenUsername = _tkmgr.ValidateToken(token);
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
