using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public void SearchByName(string name)
        {
            Pies.FirstOrDefault(x => x.Name == name);
        }
        public string CurrentCategory { get; set; }
    }
}
