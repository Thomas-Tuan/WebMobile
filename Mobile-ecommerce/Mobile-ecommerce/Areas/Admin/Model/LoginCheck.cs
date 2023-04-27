using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Mobile_ecommerce.Areas.Admin.Model
{
    public class LoginCheck
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mời nhập password")]
        public string PassWord { set; get; }
    }
}