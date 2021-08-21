using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(AppDbContext context ,IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this.context = context;
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(string category)
        {
           
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

      
    }
}
