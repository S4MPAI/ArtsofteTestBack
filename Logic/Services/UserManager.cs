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
    private IHasher hasher;

    public UserManager(IUserRepository userRepository, IHasher hasher)
    {
        this.userRepository = userRepository;
        this.hasher = hasher;
    }

    public bool CheckPassword(UserDto userDto, string password)
    {
        var hashedPassword = hasher.Create(password);

        return hashedPassword == userDto.Password;
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

    public UserDto? GetByEmail(string email)
    {
        var user = userRepository.GetByEmail(email);
        
        if (user == null)
        {
            return null;
        }

        return MapToUserDto(user);
    }

    public UserDto? GetByPhone(string phone)
    {
        var user = userRepository.GetByPhone(phone);
        
        if (user == null)
        {
            return null;
        }

        return MapToUserDto(user);
    }

    private UserDto MapToUserDto(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            FIO = user.FIO,
            Phone = user.Phone,
            Password = user.Password
        };
    }
}
