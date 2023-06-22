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
    public class CartProductsController : Controller
    {
        private BTL_MVCEntities1 db = new BTL_MVCEntities1();

        // GET: Admin/CartProducts
        public ActionResult Index()
        {
            var cartProducts = db.CartProducts.Include(c => c.Customer);
            return View(cartProducts.ToList());
        }

        // GET: Admin/CartProducts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartProduct cartProduct = db.CartProducts.Find(id);
            if (cartProduct == null)
            {
                return HttpNotFound();
            }
            return View(cartProduct);
        }

        // GET: Admin/CartProducts/Create
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.Customers, "Username", "Cname");
            return View();
        }

        // POST: Admin/CartProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,ProductNumber,Quantity")] CartProduct cartProduct)
        {
            if (ModelState.IsValid)
            {
                db.CartProducts.Add(cartProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Customers, "Username", "Cname", cartProduct.Username);
            return View(cartProduct);
        }

        // GET: Admin/CartProducts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartProduct cartProduct = db.CartProducts.Find(id);
            if (cartProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Customers, "Username", "Cname", cartProduct.Username);
            return View(cartProduct);
        }

        // POST: Admin/CartProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,ProductNumber,Quantity")] CartProduct cartProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.Customers, "Username", "Cname", cartProduct.Username);
            return View(cartProduct);
        }

        // GET: Admin/CartProducts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartProduct cartProduct = db.CartProducts.Find(id);
            if (cartProduct == null)
            {
                return HttpNotFound();
            }
            return View(cartProduct);
        }

        // POST: Admin/CartProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CartProduct cartProduct = db.CartProducts.Find(id);
            db.CartProducts.Remove(cartProduct);
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
