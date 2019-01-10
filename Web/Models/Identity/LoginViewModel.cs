using System.ComponentModel.DataAnnotations;

namespace Web.Models.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Укажите логин или email")]
        [RegularExpression(@"[A-Za-z0-9\.\-_!@]{3,25}",
            ErrorMessage = "Вы ввели неверный логин или email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [RegularExpression(@"[\S]{5,50}",
            ErrorMessage = "Вы ввели неверный пароль")]
        public string Password { get; set; }
    }
}
