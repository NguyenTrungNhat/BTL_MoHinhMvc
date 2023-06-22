using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_MoHinhMvc.Models;

namespace BTL_MoHinhMvc.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private BTL_MVCEntities1 db = new BTL_MVCEntities1();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        public ActionResult getAllData()
        {
            var products = db.Products.Include(p => p.Category);
            return Json(products.Select(s=>new {s.ProductNumber,s.Pname,s.Pimage,s.Price }).ToList(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult inputData()
        {
            ViewBag.CategoryNumber = new SelectList(db.Categories, "CategoryNumber", "CategoryName");
            return View();
        }

        [HttpPost]
      
        public ActionResult inputData([Bind(Include = "ProductNumber,CategoryNumber,Pname,Price,Details,Availablity")] Product product,
            HttpPostedFileBase file1)
        {
            product.CategoryNumber = 1;
            product.Details = "";
            product.Availablity = 1;
            string _FileName = "";
            string _path = "";
            try
            {
                if (file1 == null)
                {
                    _FileName = "noimg.jpg";
                }
                else
                {
                    if (file1.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file1.FileName);
                        _path = Path.Combine(Server.MapPath("~/FileUpload"), _FileName);
                        file1.SaveAs(_path);
                    }
                }
            }
            catch
            {

            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.Pimage = _FileName;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return Json(new {msg = true },JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }
                
            }

            ViewBag.CategoryNumber = new SelectList(db.Categories, "CategoryNumber", "CategoryName", product.CategoryNumber);
            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult viewData()
        {
            return View();
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryNumber = new SelectList(db.Categories, "CategoryNumber", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductNumber,CategoryNumber,Pname,Price,Details,Availablity")] Product product,
            HttpPostedFileBase file1)
        {
            string _FileName = "";
            string _path = "";
            try
            {
                if(file1 == null)
                {
                    _FileName = "noimg.jpg";
                }
                else
                {
                    if (file1.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file1.FileName);
                        _path = Path.Combine(Server.MapPath("~/FileUpload"), _FileName);
                        file1.SaveAs(_path);
                    }
                }
            }
            catch
            {
                
            }

            if (ModelState.IsValid)
            {
                product.Pimage = _FileName;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryNumber = new SelectList(db.Categories, "CategoryNumber", "CategoryName", product.CategoryNumber);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryNumber = new SelectList(db.Categories, "CategoryNumber", "CategoryName", product.CategoryNumber);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductNumber,CategoryNumber,Pname,Price,Details,Availablity")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryNumber = new SelectList(db.Categories, "CategoryNumber", "CategoryName", product.CategoryNumber);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
