﻿using BusinessLayer.Abstract;
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
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                f.ProductImage = newimagename;
            }
            f.ProductName = p.Name;
            f.ProductPrice = p.Price;
            f.ProductQuantity = p.Stock;
            f.ProductComment = p.Description;
            pm.ProductAdd(f);
            return RedirectToAction("Index");
        }

        public ActionResult GetProducts(string key)
        {
            var test = pm.GetList();
            var products = pm.GetList().Where(a => a.ProductKey == key || a.ProductName.Contains(key)).ToList();
            return View(products);
        }
    }
}