using System;
using System.Collections.Generic;
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
            var swipeOutRecorded = _context.UserSwipeDetails.LastOrDefault(x => 
            x.UserId.Equals(userDetails.UserId) && 
            x.SwipeOutTime == new DateTime().Date
            );

            if(swipeOutRecorded != null){
               
            }
            else{

            }
            
        }

    }
}
