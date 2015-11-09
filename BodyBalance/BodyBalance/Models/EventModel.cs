using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class EventModel
    {
        public string EventId { get; set; }
        public string name { get; set; }
        public decimal Duration { get; set; }
        public decimal MaxNb { get; set; }
        public decimal Price { get; set; }
        public string RoomId { get; set; }
        public string ActivityId { get; set; }
        public string ContributorId { get; set; }
        public string ManagerId { get; set; }
        public string Type { get; set; }
        public System.DateTime EventDate { get; set; }
    }
}