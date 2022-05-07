using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEO.Controllers
{
    public class CombineController : Controller
    {
        CombineManager cm = new CombineManager(new EfCombineDal());

        public ActionResult ContentByCombine(int combineId)
        {
            var combineValues = cm.GetList().Where(a => a.CombineID == combineId).ToList();
            return View(combineValues);
        }

        public ActionResult ContentByCombinee(int id)
        {
            var contentValues = cm.GetByID(id);
            return View(contentValues);
        }
    }
}