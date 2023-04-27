﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mobile_ecommerce.Models.ViewModel.Customer
{
    public class Create
    {
        public Create()
        {
            Image = "~/Asset/Admin/img/add.jpg";
        }
        [Display(Name = "Mã KH")]
        public int MaKH { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên khách hàng")]
        [Display(Name = "Tên khách hàng")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập SĐT")]
        [Display(Name = "SĐT")]
        public string Phone { get; set; }
    
        [Display(Name = "Giới tính")]
        public int Gender { get; set; }
        [Display(Name = "Hình khách hàng")]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}