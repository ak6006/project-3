using project_3.Models;
using System;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Threading.Tasks;

namespace project_3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CSV()
        {
            var Products = db.SP_Product_To_ComboBox();
            ViewBag.ProductId = new SelectList(Products, "product_id", "productName");
            return View();
        }

        [HttpPost]
        public ActionResult CSV(CSVModel model)
        {
            
            var Products = db.SP_Product_To_ComboBox();
            //db.Database.CommandTimeout = 300;
            ViewBag.ProductId = new SelectList(Products, "product_id", "productName");
            var ProductName = db.products.Find(model.ProductId).productName;
            var File = db.SP_barcode_generate_temp(model.ProductId, model.Number, User.Identity.GetUserId());
            string FileName = model.Number + "_" + ProductName + "_" + DateTime.Now.ToString("dd/MM/yyyy");
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Barcode";
        

            int row = 2;
            foreach (var item in File)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.ToString();
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
            return View();
        }

        public ActionResult Accounting()
        {
            return View();
        }
        public ActionResult Production()
        {
            return View();
        }
        public ActionResult Purchases()
        {
            return View();
        }
        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult System()
        {
            return View();
        }
        public ActionResult Stores()
        {
            return View();
        }
        public ActionResult Sales()
        {
            return View();
        }

        public ActionResult Sales2()
        {
            return View();
        }
        public ActionResult Resources()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}