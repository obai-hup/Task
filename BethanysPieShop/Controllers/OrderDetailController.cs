using AspNetCore.Reporting;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class OrderDetailController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppDbContext context;

        public OrderDetailController(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public  IActionResult Index()
        {
            var Order = context.OrderDetails.OrderByDescending(x => x.OrderId)
                                           .Include(x => x.Pie);

            return View( Order.ToList());
        }
        public IActionResult Print()
        {
            var dt = new DataTable();
            //  dt = GetPieList();
            string mimetype = "";
            int extension = 1;
            var path = $"{this.webHostEnvironment.WebRootPath}\\Reports\\OrderReport.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
           //   parameters.Add("para2", "Orders in Pie Shop");
            LocalReport localReport = new LocalReport(path);
            var Order = context.OrderDetails;
            localReport.AddDataSource("DataSet2", Order);
            // localReport.AddDataSource("_BethanysPieShop_1ED06986_5F07_4A1C_85B9_D9F3F477BFF5DataSet", dt);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);


            return File(result.MainStream, "application/pdf");



        }
    }
}
