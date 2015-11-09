using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class ActivityModel
    {
        public string ActivityId { get; set; }
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "Maximum lenght for short description is 20")]
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string ManagerId { get; set; }
    }
}