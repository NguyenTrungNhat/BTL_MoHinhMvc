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
    public class OrderProductsController : Controller
    {
        private BTL_MVCEntities1 db = new BTL_MVCEntities1();

        // GET: Admin/OrderProducts
        public ActionResult Index()
        {
            var orderProducts = db.OrderProducts.Include(o => o.Order).Include(o => o.Product);
            return View(orderProducts.ToList());
        }

        // GET: Admin/OrderProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // GET: Admin/OrderProducts/Create
        public ActionResult Create()
        {
            ViewBag.OrderNumber = new SelectList(db.Orders, "OrderNumber", "Username");
            ViewBag.ProductNumber = new SelectList(db.Products, "ProductNumber", "Pname");
            return View();
        }

        // POST: Admin/OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderNumber,ProductNumber,Quantity")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                db.OrderProducts.Add(orderProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderNumber = new SelectList(db.Orders, "OrderNumber", "Username", orderProduct.OrderNumber);
            ViewBag.ProductNumber = new SelectList(db.Products, "ProductNumber", "Pname", orderProduct.ProductNumber);
            return View(orderProduct);
        }

        // GET: Admin/OrderProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderNumber = new SelectList(db.Orders, "OrderNumber", "Username", orderProduct.OrderNumber);
            ViewBag.ProductNumber = new SelectList(db.Products, "ProductNumber", "Pname", orderProduct.ProductNumber);
            return View(orderProduct);
        }

        // POST: Admin/OrderProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderNumber,ProductNumber,Quantity")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderNumber = new SelectList(db.Orders, "OrderNumber", "Username", orderProduct.OrderNumber);
            ViewBag.ProductNumber = new SelectList(db.Products, "ProductNumber", "Pname", orderProduct.ProductNumber);
            return View(orderProduct);
        }

        // GET: Admin/OrderProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // POST: Admin/OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            db.OrderProducts.Remove(orderProduct);
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
