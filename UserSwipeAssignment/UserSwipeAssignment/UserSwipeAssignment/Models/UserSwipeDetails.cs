using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserSwipeAssignment.Models
{
    public class UserSwipeDetails
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> SwipeInTime { get; set; }
        public Nullable<System.DateTime> SwipeOutTime { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int UserId { get; set; }
        public Nullable<System.TimeSpan> InTimeDuration { get; set; }
        public Nullable<System.TimeSpan> OutTimeDuration { get; set; }
    }
}