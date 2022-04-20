using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using UserSwipeAssignment.Controllers;
using UserSwipeAssignment.Models;

namespace UserSwipeAssignment.Repo.SwipeRepo
{
    public interface ISwipeRepo
    {
        HttpStatusCode DoSwipeIn(SwipeInUserDetails userDetails);
        HttpStatusCode DoSwipeOut(SwipeInUserDetails userDetails);
        void MapCardEmployee(EmpMapping data);
    }
}