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

        [HttpPost]
        public void SwipeIn(SwipeInUserDetails userDetails)
        {

           //check if there's any swipe-out recorded for that day
            var swipeOutRecorded = _context.UserSwipeDetails.Where(x =>
            x.UserId.Equals(userDetails.UserId)
            &&
            DbFunctions.TruncateTime(x.SwipeOutTime) == userDetails.date.Date
            ).ToList().OrderByDescending(x=>x.Id).FirstOrDefault();

            UserSwipeDetail record = new UserSwipeDetail();
            record.UserId = userDetails.UserId;
            record.CreatedDate = new DateTime();
            record.SwipeInTime = userDetails.date;

            //existing swipe-out found for that day
            if (swipeOutRecorded != null)
            {
                //deduct previous swipe -out from the current “datetime” passed in the API
                TimeSpan ts = userDetails.date.Subtract(Convert.ToDateTime(swipeOutRecorded.SwipeOutTime));
                record.OutTimeDuration = swipeOutRecorded.OutTimeDuration + ts;
            }
            else
            {
                //existing swipe-out NOT FOUND for that day
                record.OutTimeDuration = new TimeSpan(0, 0, 0);
                record.InTimeDuration = new TimeSpan(0, 0, 0);
            }

            _context.UserSwipeDetails.Add(record);
            _context.SaveChanges();

        }

    }
}
