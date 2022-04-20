using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserSwipeAssignment.Models;
using UserSwipeAssignment.Repo.SwipeRepo;
using UserSwipeAssignment.UserRepo;
using UserSwipeAssignment.Utilities;

namespace UserSwipeAssignment.Controllers
{
    public class SwipeController : ApiController
    {
        IUserRepository _usersRepository;
        TokenManager _tkmgr;
        ISwipeRepo _swipeRepo;

        public SwipeController(IUserRepository u, TokenManager t, ISwipeRepo s)
        {
            _usersRepository = u;
            _tkmgr = t;
            _swipeRepo = s;
        }

        [Route("api/swipe/SwipeIn")]
        [HttpPost]
        public HttpResponseMessage SwipeIn(SwipeInUserDetails userDetails)
        {
            if (userDetails == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                UserDetail user = _usersRepository.GetUserById(userDetails.UserId);
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User!!!");

                string tokenUsername = _tkmgr.ValidateToken(userDetails.token);

                if (!user.UserName.Equals(tokenUsername))
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Token!!!");
                }

                HttpStatusCode status = _swipeRepo.DoSwipeIn(userDetails);
                
                if (status == HttpStatusCode.Conflict)
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Please contact administrator, your swipe-out information is not available!");

                return Request.CreateResponse(HttpStatusCode.OK, "User Swiped In successfully!!!");

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal Server Error!!!");
            }
        }

        [Route("api/swipe/SwipeOut")]
        [HttpPost]
        public HttpResponseMessage SwipeOut(SwipeInUserDetails userDetails)
        {
            if (userDetails == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                UserDetail user = _usersRepository.GetUserById(userDetails.UserId);
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User!!!");

                string tokenUsername = _tkmgr.ValidateToken(userDetails.token);

                if (!user.UserName.Equals(tokenUsername))
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Token!!!");
                }

                HttpStatusCode status = _swipeRepo.DoSwipeOut(userDetails);

                if (status == HttpStatusCode.Conflict)
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Please contact administrator, your swipe-in information is not available!");

                return Request.CreateResponse(HttpStatusCode.OK, "User Swiped Out successfully!!!");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal Server Error!!!");
            }

        }

        [Route("api/swipe/mapCardEmp")]
        [HttpPost]
        public HttpResponseMessage MapCardEmployee(EmpMapping data)
        {
            if(data == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            
            try
            {
                _swipeRepo.MapCardEmployee(data);

                return Request.CreateResponse(HttpStatusCode.OK, "Employee mapped successfully!!!");
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal Server Error!!!");
            }
            
        }
    }
}
