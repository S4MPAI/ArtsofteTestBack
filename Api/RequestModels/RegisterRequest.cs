using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class RegisterRequest
{
    [Required(ErrorMessage = "Не указано ФИО")]
    [StringLength(250, ErrorMessage = "Максимальное количество символов 250")]
    public string FIO { get; set; }
    
    [Required(ErrorMessage = "Не указан номер телефона")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    [StringLength(11, ErrorMessage = "Максимальное количество символов 11")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Некорректная почта")]
    [StringLength(150, ErrorMessage = "Максимальное количество символов 150")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(20, ErrorMessage = "Максимальное количество символов 20")]
    public string Password { get; set; }

    [Compare("Password")]
    [StringLength(20, ErrorMessage = "Максимальное количество символов 20")]
    public string PasswordConfirm { get; set; }
}
