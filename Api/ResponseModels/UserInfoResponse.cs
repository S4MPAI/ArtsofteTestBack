using System.ComponentModel;

namespace Api.ResponseModels;

public record UserInfoResponse
{
    [DisplayName("ФИО пользователя")]
    public string FIO { get; init; }

    [DisplayName("Номер телефона")]
    public string Phone { get; init; }

    public string Email { get; init; }

    [DisplayName("Дата последней авторизации")]
    public DateTime LastLogin { get; init; }
}
