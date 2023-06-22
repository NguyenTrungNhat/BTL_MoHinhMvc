using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_MoHinhMvc.Models;
using System.Web.Script.Serialization;

namespace BTL_MoHinhMvc.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";
        BTL_MVCEntities1 db = new BTL_MVCEntities1();
        public ActionResult Index()
        {
            var ds = db.Products.ToList();
            return View(ds);
        }

        public PartialViewResult Menu_victicle()
        {
            var ds = db.Categories.ToList();
            return PartialView(ds);
        }

        public ActionResult Detail(int id)
        {
            var ds = db.Products.FirstOrDefault(s => s.ProductNumber == id);
            return View(ds);
        }

        public PartialViewResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        public PartialViewResult ViewProduct(int id)
        {
            var ds = db.Products.FirstOrDefault(s => s.ProductNumber == id);
            return PartialView(ds);
        }

        public ActionResult Shop()
        {
            var ds = db.Products.ToList();
            return View(ds);
        }
        public ActionResult ShopInCategory(int id)
        {
            var ds = db.Products.Where(s => s.CategoryNumber == id).ToList();
            return View(ds);
        }

        public ActionResult Block()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult html404()
        {
            return View();
        }
        public ActionResult checkout()
        {
            return View();
        }
        public ActionResult compare()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult WishList()
        {
            return View();
        }
        public ActionResult ProductDetail()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
    }
}