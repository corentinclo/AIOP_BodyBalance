using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class NotificationModel
    {
        public string NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> NotifDate { get; set; }
        public string UserId { get; set; }      
    }
}