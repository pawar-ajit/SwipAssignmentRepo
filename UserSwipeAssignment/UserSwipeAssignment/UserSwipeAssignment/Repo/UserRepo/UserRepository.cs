using UserSwipeAssignment.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserSwipeAssignment.Models;

namespace UserSwipeAssignment.UserRepo
{
    public class UserRepository : IUserRepository
    {
        SwipeAssignmentDBEntitiesNew _context = new SwipeAssignmentDBEntitiesNew();

        public UserDetail GetUserById(int userId)
        {
            UserDetail user = _context.UserDetails.FirstOrDefault(x => x.UserId == userId);

            return user;
            //return new UserDetail { UserId = 222, UserName = "pppp" };
        }

        public UserDetail GetUserByName(UserModel data)
        {
            UserDetail user = _context.UserDetails.FirstOrDefault(x => x.UserName == data.UserName && x.UserPassword == data.Password);

            return user;
        }

    }
}