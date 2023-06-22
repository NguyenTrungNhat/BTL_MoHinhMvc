using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_MoHinhMvc.Models
{
    public class OrderDetailsDao
    {
        BTL_MVCEntities1 db = null;
        public OrderDetailsDao()
        {
            db = new BTL_MVCEntities1();
        }
        public bool Insert(OrderProduct detail)
        {
            try
            {
                db.OrderProducts.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
            
        }
    }
}