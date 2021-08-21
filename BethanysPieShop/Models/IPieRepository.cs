using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
       
        Pie GetPieById(int pieId);
    }
}
