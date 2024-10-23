using Api.ValidationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class RegisterRequest
{
    [DisplayName("ФИО")]
    [Required(ErrorMessage = "Не указано ФИО")]
    [StringLength(250, ErrorMessage = "Максимальное количество символов 250")]
    public string FIO { get; set; }
    
    [DisplayName("Номер телефона")]    
    [Required(ErrorMessage = "Не указан номер телефона")]
    [RussianPhone(ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Некорректная почта")]
    [StringLength(150, ErrorMessage = "Максимальное количество символов 150")]
    public string Email { get; set; }

    [DisplayName("Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(20, ErrorMessage = "Максимальное количество символов 20")]
    public string Password { get; set; }

    [DisplayName("Подтверждение пароля")]
    [Compare("Password")]
    [StringLength(20, ErrorMessage = "Максимальное количество символов 20")]
    public string PasswordConfirm { get; set; }
}
