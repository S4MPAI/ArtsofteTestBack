namespace Logic.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string FIO { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime LastLogin { get; set; }
}
