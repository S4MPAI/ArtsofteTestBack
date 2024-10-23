using DAL.Entities;
using DAL.Interfaces;
using Logic.Dto;
using Logic.Interfaces;

namespace Logic.Services;

public class UserManager : IUserManager
{
    private IUserRepository userRepository;

    public UserManager(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public int Create(UserCreateDto userCreateDto)
    {
        var userEntity = new User() 
        { 
            FIO = userCreateDto.FIO, 
            Email = userCreateDto.Email, 
            Phone = userCreateDto.Phone, 
            Password = userCreateDto.Password 
        };

        userEntity = userRepository.Add(userEntity);

        return userEntity.Id;
    }
}
