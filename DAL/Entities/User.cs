using DAL.Base;

namespace DAL.Entities;

public class User : BaseEntity
{
    public string FIO { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime? LastLogin { get; set; }

    public User(User user)
    {
        Id = user.Id;
        FIO = user.FIO;
        Phone = user.Phone;
        Email = user.Email;
        Password = user.Password;
    }

    public User()
    {
    }
}
