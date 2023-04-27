using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobile_ecommerce.Models.EF
{
    public class Shipping
    {
        [Key]
        public int ShippingID { get; set; }
        public string ShippingMethod { get; set; }
        public int PriceShipping { get; set; }

        public Order Order { get; set; }
    }
}