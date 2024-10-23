using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Logic.Base;
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

    public Result<int> Create(UserDto userCreateDto)
    {
        var userEntity = new User() 
        { 
            FIO = userCreateDto.FIO, 
            Email = userCreateDto.Email, 
            Phone = userCreateDto.Phone, 
            Password = userCreateDto.Password 
        };

        try
        {
            userEntity = userRepository.Add(userEntity);
        }
        catch(NotUniqueValueInPropertyException ex)
        {
            return new Result<int>(new List<Error> { new Error("NotUniqueValue", ex.Message) });
        }

        return new Result<int>(userEntity.Id);
    }
}
