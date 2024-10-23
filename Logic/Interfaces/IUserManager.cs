using Logic.Base;
using Logic.Dto;

namespace Logic.Interfaces;

public interface IUserManager
{
    public Result<int> Create(UserCreateDto userCreateDto);
}
