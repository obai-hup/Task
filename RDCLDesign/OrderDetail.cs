namespace RDCLDesign
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int PieId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
    }
}
