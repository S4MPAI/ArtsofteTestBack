using Logic.Dto;

namespace Logic.Interfaces;

public interface IUserManager
{
    public int Create(UserCreateDto userCreateDto);
}
