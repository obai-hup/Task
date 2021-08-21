using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BethanysPieShop.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = " name")]
        [StringLength(50)]
        public string Name { get; set; }

      

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }

       
    }

}
