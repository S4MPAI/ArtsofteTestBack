using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Logic.Base;
using Logic.Dto;
using Logic.Interfaces;
using System.Security.Claims;

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

    public Result<int> Create(UserDto userCreateDto)
    {
        var userEntity = new User() 
        { 
            FIO = userCreateDto.FIO, 
            Email = userCreateDto.Email, 
            Phone = userCreateDto.Phone, 
            Password = hasher.Create(userCreateDto.Password) 
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
            Password = user.Password,
            LastLogin = user.LastLogin
        };
    }

    public Result<List<Claim>> ApplySignInClaims(UserDto userDto)
    {
        var userEntity = userRepository.GetByPhone(userDto.Phone);

        if (userEntity == null)
        {
            return new Result<List<Claim>>(new[] { new Error("IncorrectPhone", "Пользователя с таким номером телефона не существует") });
        }

        var dtoHashedPassword = hasher.Create(userDto.Password);
        if (dtoHashedPassword != userEntity.Password) 
        {
            return new Result<List<Claim>>(new[] { new Error("IncorrectPassword", "Неверный пароль") });
        }

        userEntity.LastLogin = DateTime.Now;
        userRepository.Update(userEntity);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.MobilePhone, userDto.Phone),
            new Claim(ClaimTypes.Email, userEntity.Email),
            new Claim(ClaimTypes.Name, userEntity.FIO)
        };

        return new Result<List<Claim>>(claims);
    }
}
