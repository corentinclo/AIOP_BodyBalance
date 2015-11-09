using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class ContributorModel : UserModel
    {
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
    }
}