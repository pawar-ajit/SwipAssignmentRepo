//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserSwipeAssignment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSwipeDetail
    {
        public int Id { get; set; }
        public System.DateTime SwipeOutTime { get; set; }
        public System.DateTime SwipeInTime { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public System.TimeSpan InTimeDuration { get; set; }
        public System.TimeSpan OutTimeDuration { get; set; }
    }
}
