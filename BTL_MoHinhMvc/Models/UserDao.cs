using BTL_MoHinhMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_MoHinhMvc.Models
{
    public class UserDao
    {
        BTL_MVCEntities1 db = null;
        public UserDao()
        {
            db = new BTL_MVCEntities1();
        }

        public string Insert(Customer entity)
        {
            db.Customers.Add(entity);
            db.SaveChanges();
            return entity.Username;
        }
        public bool CheckUserName(string userName)
        {
            return db.Customers.Count(x => x.Username == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Customers.Count(x => x.Email == email) > 0;
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Customers.SingleOrDefault(x => x.Username == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
        public Customer GetById(string userName)
        {
            return db.Customers.SingleOrDefault(x => x.Username == userName);
        }
    }
}