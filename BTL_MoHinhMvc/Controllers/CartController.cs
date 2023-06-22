using BTL_MoHinhMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BTL_MoHinhMvc.Common;

namespace BTL_MoHinhMvc.Controllers
{
    public class CartController : Controller
    {
        BTL_MVCEntities1 db = new BTL_MVCEntities1();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsoncart.SingleOrDefault(x => x.Product.ProductNumber == item.Product.ProductNumber);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        
        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession] ?? new List<CartItem>();

            var itemToRemove = sessionCart.Single(r => r.Product.ProductNumber == id);
            sessionCart.Remove(itemToRemove);

            Session[CartSession] = sessionCart;
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddItem(int productNumber, int quantity)
        {
            var product = db.Products.FirstOrDefault(s => s.ProductNumber == productNumber);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ProductNumber == productNumber))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ProductNumber == productNumber)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = (Product)product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
                
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = (Product)product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }


        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var session = (BTL_MoHinhMvc.Common.UserLogin)Session[BTL_MoHinhMvc.Common.CommonConstants.USER_SESSION];
            if(session == null)
            {
                return Redirect("/User/Login");
            }
            var order = new Order();
            order.OrderDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobie = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;
            order.Username = session.UserName;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailsDao();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderProduct();
                    orderDetail.ProductNumber = item.Product.ProductNumber;
                    orderDetail.OrderNumber = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                }
            }
            catch(Exception ex)
            {
                return Redirect("/Cart/LoiThanhToan");
            }

            return Redirect("/Cart/Success");
        }
        public ActionResult Success()
        {
            Session[CartSession] = null;
            return View();
        }
    }
}