//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTL_MoHinhMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProduct
    {
        public int OrderNumber { get; set; }
        public int ProductNumber { get; set; }
        public int Quantity { get; set; }
        public int OrderProductsID { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}