using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserSwipeAssignment.Models;

namespace UserSwipeAssignment.DAL
{
    public class UsersDAL
    {
        SwipeAssignmentDBEntitiesNew _context = new SwipeAssignmentDBEntitiesNew();

        public virtual UserDetail GetUserById(int userId)
        {
            UserDetail user = _context.UserDetails.FirstOrDefault(x => x.UserId == userId);

            return user;
            //return new UserDetail { UserId = 222, UserName = "pppp" };
        }
    }
}