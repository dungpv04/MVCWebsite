using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebsite.Models
{

    public class LoginModel
    {
        [Required(ErrorMessage = "Điền thông tin !")]
        public string username {  get; set; }
        [Required(ErrorMessage = "Điền thông tin !")]
        public string password { get; set; }

        public bool ifValid {  get; set; }
    }
}