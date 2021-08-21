namespace RDCLDesign
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pie
    {
        public int PieId { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
