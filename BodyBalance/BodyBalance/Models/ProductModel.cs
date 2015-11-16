using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal MemberReduction { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
    }
}