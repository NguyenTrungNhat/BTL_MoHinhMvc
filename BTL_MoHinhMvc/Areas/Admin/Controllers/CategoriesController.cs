using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_MoHinhMvc.Models;

namespace BTL_MoHinhMvc.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private BTL_MVCEntities1 db = new BTL_MVCEntities1();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult getAllData()
        {
            return Json(db.Categories.Select(s => new { s.CategoryNumber,s.CategoryName,s.CategoryLevel,s.ParentID}).ToList(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult inputData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult inputData([Bind(Include = "CategoryNumber,CategoryName,CategoryLevel")] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return Json(new { msg = true},JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }
                
            }

            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult viewData()
        {
            return View();
        }

        public ActionResult getData()
        {
            return View();
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CategoryNumber,CategoryName,CategoryLevel")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Add(category);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}

        [HttpPost]
        public JsonResult Create(Category l)
        {
            db.Categories.Add(l);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public JsonResult GetByID(int id)
        {
            var obj = db.Categories.Where(a => a.CategoryNumber == id).FirstOrDefault();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Category loaisanphammoi)
        {
            var old = db.Categories.Where(a => a.CategoryNumber == loaisanphammoi.CategoryNumber).FirstOrDefault();
            db.Entry(old).CurrentValues.SetValues(loaisanphammoi);
            db.SaveChanges();
            return Json(old, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CategoryNumber,CategoryName,CategoryLevel")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(category).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
