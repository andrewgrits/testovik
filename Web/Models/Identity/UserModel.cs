using System.ComponentModel.DataAnnotations;

namespace Web.Models.Identity
{
    public class UserModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [RegularExpression(@"[A-Za-z0-9\.\-_!]{3,25}",
            ErrorMessage = "Логин должен состоять из букв, цифр длиной от 3 до 25")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [RegularExpression(@"[\S]{5,50}",
            ErrorMessage = "Пароль должен состоять из букв, цифр и некоторых специальных символов длиной от 5 до 25")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,})+)$",
            ErrorMessage = "Неверный формат Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [RegularExpression(@"[A-Za-zА-Яа-я\-\s]{3,25}",
            ErrorMessage = "Имя должно состоять из букв, цифр длиной от 3 до 25")]
        public string Name { get; set; }
    }
}
