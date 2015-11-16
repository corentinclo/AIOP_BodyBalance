using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class PurchaseModel
    {
        public string PurchaseId { get; set; }
        public string UserId { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<PurchaseLineModel> PurchaseLine { get; set; }
    }
}