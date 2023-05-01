using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobile_ecommerce.Models.ViewModel.Order
{
    public class Detail
    {
        [Display(Name ="Mã hóa đơn")]
        public int OrderID { get; set; }
        [Display(Name = "Ngày tạo hóa đơn")]
        public string Ngay { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }
        [Display(Name = "Tình trạng")]
        public string Status { get; set; }
    }
}