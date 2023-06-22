using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_MoHinhMvc.Models
{
    public class RegisterModel
    {
        [Key]
        public int ID { set; get; }



        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]

        public string UserName { set; get; }


        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Cname { set; get; }


        [Display(Name = "Email")]
        public string Email { set; get; }


        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 ký tự")]
        public string Password { set; get; }


        [Display(Name = "Điện thoại")]
        public int Phone { set; get; }


        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }


        [Display(Name = "Mã code")]
        public int PostCode { set; get; }

    }
}