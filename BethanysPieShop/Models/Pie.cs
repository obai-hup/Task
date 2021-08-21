using AdminPanel.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class Pie
    {
        public int PieId { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimal Length is 2 ")]
        public string Name { get; set; }

        [Required, MinLength(10, ErrorMessage = "Minimal Length is 10 ")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        [Required, Range(1, int.MaxValue, ErrorMessage = "You Must Choose a Category")]
        public int CategoryId { get; set; }



        public Category Category { get; set; }

        [FileExtian]
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
    }
}
