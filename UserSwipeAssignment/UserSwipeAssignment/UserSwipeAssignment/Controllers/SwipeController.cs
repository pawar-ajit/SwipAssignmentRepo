using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserSwipeAssignment.Models;

namespace UserSwipeAssignment.Controllers
{
    public class SwipeController : ApiController
    {
        SwipeAssignmentDBEntities _context = new SwipeAssignmentDBEntities();

        [Route("api/swipe/getDetails")]
        [HttpGet]
        public HttpResponseMessage GetDetails()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "successfully!!!");
        }

        [Route("api/swipe/SwipeIn")]
        [HttpPost]
        public void SwipeIn(SwipeInUserDetails userDetails)
        {

           //check if there's any swipe-out recorded for that day
            var lastRecord = _context.UserSwipeDetails.Where(x =>
            x.UserId.Equals(userDetails.UserId)
            &&
            //DbFunctions.TruncateTime(x.SwipeOutTime) == userDetails.date.Date
            DbFunctions.TruncateTime(x.CreatedDate) == userDetails.date.Date
            ).ToList().OrderByDescending(x=>x.Id).FirstOrDefault();

            UserSwipeDetail record = new UserSwipeDetail();
            record.UserId = userDetails.UserId;
            record.CreatedDate = DateTime.Now;
            record.SwipeInTime = userDetails.date;

            //existing swipe-out found for that day
            if (lastRecord != null && lastRecord.SwipeOutTime != new DateTime())
            {
                //deduct previous swipe -out from the current “datetime” passed in the API
                TimeSpan ts = userDetails.date.Subtract(Convert.ToDateTime(lastRecord.SwipeOutTime));
                record.OutTimeDuration = lastRecord.OutTimeDuration + ts;
                record.InTimeDuration = lastRecord.InTimeDuration;//
            }
            else
            {
                //existing swipe-out NOT FOUND for that day
                record.OutTimeDuration = userDetails.date.TimeOfDay;
                record.InTimeDuration = new TimeSpan(0, 0, 0);
            }

            _context.UserSwipeDetails.Add(record);
            _context.SaveChanges();

        }

        [Route("api/swipe/SwipeOut")]
        [HttpPost]
        public HttpResponseMessage SwipeOut(SwipeInUserDetails userDetails)
        {
            //get that days last record
            var lastRecord = _context.UserSwipeDetails.Where(x =>
            x.UserId.Equals(userDetails.UserId)
            &&
            //DbFunctions.TruncateTime(x.SwipeInTime) == userDetails.date.Date
            DbFunctions.TruncateTime(x.CreatedDate) == userDetails.date.Date
            ).ToList().OrderByDescending(x => x.Id).FirstOrDefault();
            
            UserSwipeDetail record = new UserSwipeDetail();
            record.UserId = userDetails.UserId;
            record.CreatedDate = DateTime.Now;
            record.SwipeOutTime = userDetails.date;
            
            //existing swipe-in found for that day
            if (lastRecord != null && (lastRecord.SwipeInTime != new DateTime()))
            {
                //deduct previous swipe-in from the current “datetime” passed in the API
                TimeSpan ts = userDetails.date.Subtract(Convert.ToDateTime(lastRecord.SwipeInTime));
                record.InTimeDuration = lastRecord.InTimeDuration + ts;
                record.OutTimeDuration = lastRecord.OutTimeDuration;//As it is
                _context.UserSwipeDetails.Add(record);
                _context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "User Swiped out successfully!!!");
            }
            else if (lastRecord != null && (lastRecord.SwipeOutTime != new DateTime()))//last record is swipe out
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Please contact administrator, your swipe-in information is not available!");
            }
            else//existing swipe-in NOT FOUND for that day (got last record null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ffaasfaffaf!!!");//temp code
            }

        }
    }
}
