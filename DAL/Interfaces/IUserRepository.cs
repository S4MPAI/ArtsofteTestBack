using DAL.Entities;
using System.Runtime.InteropServices;

namespace DAL.Interfaces;

public interface IUserRepository
{
    public User Add(User user);
    public void Update(User user);
    public void Delete(User user);
    public User GetById(int id);
}
