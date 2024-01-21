using System.ComponentModel.DataAnnotations;

namespace MVCWebsite.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Bắt buộc")]
        public string username { get; set; }
        [Required(ErrorMessage = "Bắt buộc")]
        public string password { get; set; }
        [Required(ErrorMessage = "Bắt buộc")]
        public string retypePassword { get; set; }
        public bool ifValid {  get; set; }
        public bool ifMatch {  get; set; }

    }
}