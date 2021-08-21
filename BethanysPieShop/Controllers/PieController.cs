using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class PieController : Controller
    {
        private readonly AppDbContext context;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PieController(AppDbContext context ,IPieRepository pieRepository, ICategoryRepository categoryRepository , IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
          
            return View( _pieRepository.AllPies);
        }


        public IActionResult Print()
        {
            var dt = new DataTable();
          //  dt = GetPieList();
            string mimetype = "";
            int extension = 1;
            var path = $"{this.webHostEnvironment.WebRootPath}\\Reports\\PieReport.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
         //  parameters.Add("param1", "Wellcome To Pie Shop");
            LocalReport localReport = new LocalReport(path);
            var pies = _pieRepository.AllPies;
            localReport.AddDataSource("DataSet1", pies);
           // localReport.AddDataSource("_BethanysPieShop_1ED06986_5F07_4A1C_85B9_D9F3F477BFF5DataSet", dt);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);


            return File(result.MainStream, "application/pdf");



        }

       
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(I => I.CategoryId), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pie pies)
        {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(I => I.CategoryId), "CategoryId", "CategoryName");


            if (ModelState.IsValid)
            {

                var Pro = await context.Pies.FirstOrDefaultAsync(x => x.Name == pies.Name);
                if (Pro != null)
                {
                    ModelState.AddModelError("", "The products Already Exist.");
                    return View(pies);
                }
                string ImageName = "noimage.png";
                if (pies.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Media/Products");
                    ImageName = Guid.NewGuid().ToString() + "_" + pies.ImageUpload.FileName;
                    string FilePath = Path.Combine(uploadDir, ImageName);
                    FileStream fs = new FileStream(FilePath, FileMode.Create);
                    await pies.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                }

                pies.ImageUrl = ImageName;

                context.Add(pies);
                await context.SaveChangesAsync();
                TempData["Success"] = "The products Has been Added!";
                return RedirectToAction("Index");
            }

            return View(pies);

        }

        public async Task<IActionResult> Details(int id)

        {
            var products = await context.Pies.Include(c => c.Category).FirstOrDefaultAsync(x => x.CategoryId == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Pie pie = await context.Pies.FindAsync(id);

            if (pie == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(I => I.CategoryId), "CategoryId", "CategoryName", pie.CategoryId);

            return View(pie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Pie products)
        {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(I => I.CategoryId), "Id", "Name", products.CategoryId);

            if (ModelState.IsValid)
            {


                var Pro = await context.Pies.Where(x => x.PieId != id).FirstOrDefaultAsync(s => s.Name == products.Name);
                if (Pro != null)
                {
                    ModelState.AddModelError("", "The products Already Exist.");
                    return View(products);
                }

                if (products.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Media/Products");
                    if (!string.Equals(products.ImageUrl, "noimage.png"))
                    {
                        string OldImage = Path.Combine(uploadDir, products.ImageUrl);
                        if (System.IO.File.Exists(OldImage))
                        {
                            System.IO.File.Delete(OldImage);
                        }

                    }
                    string ImageName = Guid.NewGuid().ToString() + "_" + products.ImageUpload.FileName;
                    string FilePath = Path.Combine(uploadDir, ImageName);
                    FileStream fs = new FileStream(FilePath, FileMode.Create);
                    await products.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    products.ImageUrl = ImageName;
                }


                context.Update(products);
                await context.SaveChangesAsync();
                TempData["Success"] = "The products Has been Edied!";
                return RedirectToAction("Index");
            }

            return View(products);

        }

        public async Task<IActionResult> Delete(int id)
        {
            Pie products = await context.Pies.FindAsync(id);

            if (products == null)
            {
                TempData["Error"] = "The Page Not Found!";
            }
            else
            {
                if (!string.Equals(products.ImageUrl, "noimage.png"))
                {
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Image");

                    string OldImage = Path.Combine(uploadDir, products.ImageUrl);
                    if (System.IO.File.Exists(OldImage))
                    {
                        System.IO.File.Delete(OldImage);
                    }

                }
                context.Pies.Remove(products);
                await context.SaveChangesAsync();

                TempData["Success"] = "The products Has been Deleted!";
            }

            return RedirectToAction("Index");
        }

    }
}