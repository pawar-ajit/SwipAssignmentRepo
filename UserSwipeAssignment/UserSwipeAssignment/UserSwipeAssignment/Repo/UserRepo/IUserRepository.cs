using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSwipeAssignment.Models;

namespace UserSwipeAssignment.UserRepo
{
    public interface IUserRepository
    {
        UserDetail GetUserById(int userId);
    }
}
