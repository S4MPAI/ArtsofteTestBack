using DAL.Entities;
using System.Runtime.InteropServices;

namespace DAL.Interfaces;

public interface IUserRepository
{
    public User Add(User user);
    public User Update(User user);
    public void Delete(User user);
    public User? GetByEmail(string email);
    public User? GetByPhone(string phone);
}
