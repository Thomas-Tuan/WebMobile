using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mobile_ecommerce.Models.EF
{
    public class ReviewPro
    {
        [Key]
        public int ReviewProID { get; set; }
        public int RateValue { get; set; }
        public string Comment { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}