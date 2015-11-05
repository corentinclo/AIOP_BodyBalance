using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class TokenModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public System.DateTime ExpireDate { get; set; }
    }
}