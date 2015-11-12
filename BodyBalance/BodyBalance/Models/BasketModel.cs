using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class BasketModel
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}