using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mobile_ecommerce.Models.EF
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Total { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int ShippingID { get; set; }
        public ICollection<Shipping> Shippings { get; set; }

     
        public int OrderStatusID { get; set; }
        public ICollection<OrderStatus> OrderStatuses { get; set; }

        public string VoucherID { get; set; }
        public Voucher Vouchers { get; set; }
    }
}