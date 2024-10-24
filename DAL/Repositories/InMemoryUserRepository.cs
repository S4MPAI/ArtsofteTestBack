using DAL.Base;
using DAL.Contexts;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;

namespace DAL.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private InMemorySet<User> users;

    public InMemoryUserRepository(InApplicationMemoryContext context)
    {
        users = context.Users;
    }

    public User Add(User user)
    {
        var newUser = new User(user);

        CheckUserOnUniqueValues(newUser);

        users.Append(newUser);
        return newUser;
    }

    private void CheckUserOnUniqueValues(User newUser)
    {
        foreach (var entity in users)
        {
            if (entity.Phone == newUser.Phone)
            {
                throw new NotUniqueValueInPropertyException(typeof(User), nameof(User.Phone), entity.Phone);
            }
            if (entity.Email == newUser.Email)
            {
                throw new NotUniqueValueInPropertyException(typeof(User), nameof(User.Email), entity.Email);
            }
        }
    }

    public void Delete(User user)
    {
        var newDeleteUser = new User(user);
        users.Delete(newDeleteUser);
    }

    public User? GetByEmail(string email)
    {
        return users.FirstOrDefault(x => x.Email == email);
    }

    public void Update(User user)
    {
        var newUpdateUser = new User(user);
        users.Update(newUpdateUser);
    }

    public User? GetByPhone(string phone)
    {
        return users.FirstOrDefault(x => x.Phone == phone);
    }
}
