using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using UserSwipeAssignment.Controllers;
using UserSwipeAssignment.Models;

namespace UserSwipeAssignment.Repo.SwipeRepo
{
    public class SwipeRepo : ISwipeRepo
    {
        SwipeAssignmentDBEntitiesNew _context = new SwipeAssignmentDBEntitiesNew();

        public HttpStatusCode DoSwipeIn(SwipeInUserDetails userDetails)
        {
            //check if there's any swipe-out recorded for that day
            var lastRecord = _context.UserSwipeDetails.Where(x =>
            x.UserId.Equals(userDetails.UserId)
            &&
            //DbFunctions.TruncateTime(x.SwipeOutTime) == userDetails.date.Date
            DbFunctions.TruncateTime(x.CreatedDate) == userDetails.date.Date
            ).ToList().OrderByDescending(x => x.Id).FirstOrDefault();

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
            else if (lastRecord != null && lastRecord.SwipeInTime != new DateTime())//previous record is swipeIn
            {
                return HttpStatusCode.Conflict;
            }
            else
            {
                //existing swipe-out NOT FOUND for that day
                record.OutTimeDuration = userDetails.date.TimeOfDay;
                record.InTimeDuration = new TimeSpan(0, 0, 0);
            }

            _context.UserSwipeDetails.Add(record);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DoSwipeOut(SwipeInUserDetails userDetails)
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

                return HttpStatusCode.OK;
            }
            else if (lastRecord != null && (lastRecord.SwipeOutTime != new DateTime()))//last record is swipe out
            {
                return HttpStatusCode.Conflict;
            }
            else//existing swipe-in NOT FOUND for that day (got last record null)
            {
                DateTime previousDate = userDetails.date.AddDays(-1);

                var previousDayLastRecord = _context.UserSwipeDetails.Where(x =>
                x.UserId.Equals(userDetails.UserId)
                &&
                DbFunctions.TruncateTime(x.CreatedDate) == previousDate.Date
                ).ToList().OrderByDescending(x => x.Id).FirstOrDefault();

                if (previousDayLastRecord?.SwipeOutTime != new DateTime())//previous day swipe out present
                {
                    return HttpStatusCode.Conflict;
                }
                else //previous day swipe out not present
                {

                    //b. We can update the swipe-out time for the previous day as 11:59:59 and calculate the IN-Time.
                    UserSwipeDetail previousDayRecord = new UserSwipeDetail();
                    previousDayRecord.UserId = userDetails.UserId;
                    previousDayRecord.CreatedDate = DateTime.Now;
                    previousDayRecord.SwipeOutTime = previousDate.Date + new TimeSpan(23, 59, 59);
                    TimeSpan ts = previousDayRecord.SwipeOutTime.Subtract(Convert.ToDateTime(previousDayLastRecord.SwipeInTime));
                    previousDayRecord.InTimeDuration = previousDayLastRecord.InTimeDuration + ts;
                    previousDayRecord.OutTimeDuration = previousDayLastRecord.OutTimeDuration;
                    _context.UserSwipeDetails.Add(previousDayRecord);

                    //For the present/current day/date we can take swipe-in time as 00:00:00 
                    //and swipe-out time as the “datetime” which is provided as input 
                    //and the IN-Time can be calculated accordingly.
                    UserSwipeDetail currentDayInRecord = new UserSwipeDetail();
                    currentDayInRecord.UserId = userDetails.UserId;
                    currentDayInRecord.CreatedDate = DateTime.Now;
                    currentDayInRecord.SwipeInTime = userDetails.date.Date + new TimeSpan(0, 0, 0);
                    _context.UserSwipeDetails.Add(currentDayInRecord);

                    UserSwipeDetail currentDayOutRecord = new UserSwipeDetail();
                    currentDayOutRecord.UserId = userDetails.UserId;
                    currentDayOutRecord.CreatedDate = DateTime.Now;
                    TimeSpan timespan = userDetails.date.Subtract(userDetails.date.Date + new TimeSpan(0, 0, 0));
                    currentDayOutRecord.InTimeDuration = timespan;
                    currentDayOutRecord.SwipeInTime = new DateTime();
                    currentDayOutRecord.SwipeOutTime = userDetails.date;
                    _context.UserSwipeDetails.Add(currentDayOutRecord);

                    _context.SaveChanges();

                    return HttpStatusCode.OK;
                }

            }
        }

        public void MapCardEmployee(EmpMapping data)
        {
            Mapping record = new Mapping();
            record.CardId = data.CardId;
            record.EmployeeId = data.EmployeeId;
            record.UserType = data.UserType;

            _context.Mappings.Add(record);
            _context.SaveChanges();
        }
    }
}