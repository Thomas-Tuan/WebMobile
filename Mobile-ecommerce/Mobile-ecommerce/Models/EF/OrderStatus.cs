using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobile_ecommerce.Models.EF
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusID { get; set; }
        public string Status { get; set; }
     
        public Order Order { get; set; }
    }
}