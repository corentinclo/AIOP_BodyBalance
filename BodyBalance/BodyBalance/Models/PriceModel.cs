using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class PriceModel
    {
        public string ProductId { get; set; }
        public System.DateTime DatePrice { get; set; }
        public decimal ProductPrice { get; set; }
    }
}