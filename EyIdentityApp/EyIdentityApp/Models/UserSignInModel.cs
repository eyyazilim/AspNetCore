using System.ComponentModel.DataAnnotations;

namespace EyIdentityApp.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre gereklidir!")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
