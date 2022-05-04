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

namespace ProjectSEO.Controllers
{
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        public ActionResult Index()
        {
            var productValues = pm.GetList();
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
            return RedirectToAction("Index");
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
    }
}