using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class PurchaseLineModel
    {
        public string PurchaseId { get; set; }
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }
        public Nullable<System.DateTime> ValidationDate { get; set; }
    }
}