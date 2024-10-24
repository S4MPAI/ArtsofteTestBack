using Logic.Base;
using Logic.Dto;

namespace Logic.Interfaces;

public interface IUserManager
{
    public Result<int> Create(UserDto userDto);
    public UserDto? GetByEmail(string email);
    public UserDto? GetByPhone(string phone);
    public bool CheckPassword(UserDto userDto, string password);
}
