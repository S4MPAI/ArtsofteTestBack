using Logic.Base;
using Logic.Dto;
using System.Security.Claims;

namespace Logic.Interfaces;

public interface IUserManager
{
    public Result<int> Create(UserDto userDto);
    public UserDto? GetByEmail(string email);
    public UserDto? GetByPhone(string phone);
    public Result<List<Claim>> ApplySignInClaims(UserDto userDto);
}
