using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ProjectSEO.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProjectSEO.Controllers
{
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        public ActionResult Index(int categoryId)
        {
            var productValues = pm.GetList().Where(a => a.CategoryID == categoryId).ToList();
            return View(productValues);
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();
            ViewBag.vlc = valueCategory;
            var productValue = pm.GetByID(id);
            return View(productValue);
        }

        [HttpPost]
        public ActionResult EditProduct(Product p)
        {
            pm.ProductUpdate(p);
            return RedirectToAction("Index", new { categoryId = p.CategoryID });
        }

        DbSEO c = new DbSEO();
        public ActionResult GetAllProduct(string p)
        {
            var values = from x in c.Products select x;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(y => y.ProductName.Contains(p));
            }
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult AddProduct(Product p)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductAdd p)
        {
            Product f = new Product();
            if (p.ImageURL != null)
            {
                var extension = Path.GetExtension(p.ImageURL.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "ProjectSEO/images/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                f.ProductImage = newimagename;
            }
            f.ProductName = p.Name;
            f.ProductPrice = p.Price;
            f.ProductQuantity = p.Stock;
            f.ProductComment = p.Description;
            f.CategoryID = p.CategoryId;
            pm.ProductAdd(f);
            return RedirectToAction("Index");
        }

        public ActionResult GetProducts(string key)
        {
            var test = pm.GetList();
            var products = pm.GetList().Where(a => a.ProductKey == key || a.ProductName.Contains(key)).ToList();
            return View(products);
        }

        [Route("Product/ContentByProduct/{id}/{name}/{key}")]
        public ActionResult ContentByProduct(int id)
        {
            var contentValues = pm.GetListByProductID(id);
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase excelFile)
        {
            if (excelFile == null
            || excelFile.ContentLength == 0)
            {
                ViewBag.Error = "Lütfen dosya seçimi yapınız.";
                return View();
            }
            else
            {
                if (excelFile.FileName.EndsWith("xls")
                || excelFile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/" + excelFile.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    excelFile.SaveAs(path);
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    List<Product> localList = new List<Product>();

                    for (int i = 2; i <= range.Rows.Count; i++)
                    {
                        Product lm = new Product();
                        lm.ProductID = Convert.ToInt32(((Excel.Range)range.Cells[i, 1]).Text);
                        lm.ProductName = ((Excel.Range)range.Cells[i, 2]).Text;
                        lm.ProductComment = ((Excel.Range)range.Cells[i, 3]).Text;
                        lm.ProductKey = ((Excel.Range)range.Cells[i, 4]).Text;
                        localList.Add(lm);
                    }

                    application.Quit();
                    ViewBag.Model = localList;
                    return View("ListExcel");
                }
                else
                {
                    return View();
                }
            }

        }
    }
}