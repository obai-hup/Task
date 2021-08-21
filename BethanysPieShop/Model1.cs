using BethanysPieShop.Models;
using System.Data.Entity;

namespace BethanysPieShop
{
    public partial class Model1 : System.Data.Entity.DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual System.Data.Entity.DbSet<Pie> Pies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
