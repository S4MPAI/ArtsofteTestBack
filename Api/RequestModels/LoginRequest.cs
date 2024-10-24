using Api.ValidationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class LoginRequest
{
    [DisplayName("Номер телефона")]
    [Required(ErrorMessage = "Не указан номер телефона")]
    [RussianPhone(ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; set; }

    [DisplayName("Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [MaxLength(20, ErrorMessage = "Максимальное количество символов 20")]
    public string Password { get; set; }
}
