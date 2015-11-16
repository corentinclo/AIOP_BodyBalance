using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class RolesModel
    {
        public bool IsAdmin { get; set; }
        public bool IsContributor { get; set; }
        public bool IsManager { get; set; }
        public bool IsMember { get; set; }
    }
}