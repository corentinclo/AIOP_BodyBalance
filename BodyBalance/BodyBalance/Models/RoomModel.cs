using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class RoomModel
    {
        public string RoomId { get; set; }
        public string Name { get; set; }
        public decimal Superficy { get; set; }
        public Nullable<decimal> MaxNb { get; set; }
    }
}