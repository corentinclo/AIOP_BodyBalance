using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string PC { get; set; }
        public string Town { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
    }
}