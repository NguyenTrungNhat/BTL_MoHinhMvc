using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_MoHinhMvc.Models
{
    public class OrderDao
    {
        BTL_MVCEntities1 db = null;
        public OrderDao()
        {
            db = new BTL_MVCEntities1();
        }
        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderNumber;
        }
    }
}