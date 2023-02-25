using System.ComponentModel.DataAnnotations;

namespace EyCoreYediIdentity.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir!")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Lütfen bir email formatı giriniz!")]
        [Required(ErrorMessage = "E-Posta adresi gereklidir!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola alanı gereklidir!")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor!")]
        public string ConfirmPassword { get; set; }
    }
}
